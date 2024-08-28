using System;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.Application.Auth;
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

        public async Task<(string, string, string, string, Guid)> Register(string name, string password, string email)
        {

            var findUser = await _usersRepository.GetByEmail(email);
            if (findUser != null)
            {
                throw new Exception("User already exists");
            }

            var hashedPassword = _passwordHasher.Generate(password);
            

            var user = User.Create(Guid.NewGuid(), name, hashedPassword, email);
            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            var token = RefreshToken.Create(Guid.NewGuid(), refreshToken, DateTime.Now);
            user.RefreshTokenId = token.Id;
            await _refreshTokensRepository.Add(token);
            await _usersRepository.Add(user);

            return (accessToken, refreshToken, user.Name, user.Email, user.Id );
        }

        public async Task<(string, string, string, string, Guid)> Login(string email, string password)
        {
            var findUser = await _usersRepository.GetByEmail(email);

           

            var result = _passwordHasher.Verify(password, findUser.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var (accessToken, refreshToken) = _jwtProvider.Generate(findUser);
            await _refreshTokensRepository.UpdateToken(findUser.RefreshTokenId, refreshToken);
            return (accessToken, refreshToken, findUser.Name, findUser.Email, findUser.Id);

        }

        public async Task<(string, string, string, string, Guid)> Refresh(string oldRefreshToken)
        {
            var principal = _jwtProvider.ValidateRefreshToken(oldRefreshToken);

            var oldRefreshTokenInDb = await _refreshTokensRepository.GetRefreshTokenAsync(oldRefreshToken);
            if(principal == null || oldRefreshTokenInDb == null)
            {
                throw new Exception();
            }

            var userId = principal.FindFirst("userId")?.Value;

            if(string.IsNullOrEmpty(userId)) { throw new Exception(); }
            var user = await _usersRepository.GetById(userId);
            if (user == null) { throw new Exception(); }

            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            await _refreshTokensRepository.UpdateToken(user.RefreshTokenId, refreshToken);
            return (accessToken, refreshToken, user.Name, user.Email, user.Id);

        }




    }
}

