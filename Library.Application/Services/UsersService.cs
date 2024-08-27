using System;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.Application.Auth;
namespace Library.Application.Services
{
	public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IPasswordHasher passwordHasher, IUsersRepository usersRepository, IJwtProvider jwtProvider)
		{

            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;

        }

        public async Task Register(string name, string password, string email)
        {

            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), name, hashedPassword, email);

            await _usersRepository.Add(user);
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

    }
}

