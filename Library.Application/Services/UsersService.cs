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

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.Generate(user);

            return ""  /*token*/;
        }
         
        public async Task<(string, string)> GenerateTokens()
        {

            return ("", "");
        }


    }
}

