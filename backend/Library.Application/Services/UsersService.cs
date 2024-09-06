using System;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.Application.Auth;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace Library.Application.Services
{
	public class UsersService: IUsersService
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IPasswordHasher passwordHasher, IUsersRepository usersRepository, IJwtProvider jwtProvider, IRefreshTokensRepository refreshTokensRepository)
		{

            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _refreshTokensRepository = refreshTokensRepository;

        }

        public async Task<(string, string)> Register(string name, string password, string email)
        {

            var findUser = await _usersRepository.GetByEmail(email);
            if (findUser != null)
            {
                throw new Exception("User already exists");
            }

            var hashedPassword = _passwordHasher.Generate(password);
            

            var user = User.Create(Guid.NewGuid(), name, hashedPassword, email, "User");
            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            var token = RefreshToken.Create(Guid.NewGuid(), refreshToken, DateTime.Now);
            user.RefreshTokenId = token.Id;
            await _refreshTokensRepository.Add(token);
            await _usersRepository.Add(user);

            return (accessToken, refreshToken );
        }

        public async Task<(string, string, User?)> Login(string email, string password)
        {
            var findUser = await _usersRepository.GetByEmail(email);

            if (findUser == null) { throw new Exception(); }


            var result = _passwordHasher.Verify(password, findUser.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var (accessToken, refreshToken) = _jwtProvider.Generate(findUser);
            await _refreshTokensRepository.UpdateToken(findUser.RefreshTokenId, refreshToken);
            return (accessToken, refreshToken, findUser);

        }

        public async Task<(string, string, User)> Refresh(string oldRefreshToken, string userId)
        {
            var goodToken = _jwtProvider.ValidateRefreshToken(oldRefreshToken);

            if (!goodToken)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var oldRefreshTokenInDb = await _refreshTokensRepository.GetRefreshTokenAsync(oldRefreshToken);
            if (oldRefreshTokenInDb == null)
            {
                throw new SecurityTokenException("Refresh token not found in database.");
            }

           

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException("Invalid token, userId claim is missing.");
            }

            var user = await _usersRepository.GetById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            await _refreshTokensRepository.UpdateToken(user.RefreshTokenId, refreshToken);

            return (accessToken, refreshToken, user);
        }

        public async Task<User> CheckAccess(string userId)
        {

            var user = await _usersRepository.GetById(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            //var newAccessToken = _jwtProvider.GenerateAccess(user);


            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user;
        }



    }
}

