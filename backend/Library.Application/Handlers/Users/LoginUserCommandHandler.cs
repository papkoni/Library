using System;
using Library.Application.Auth;
using Library.Application.Commands.Users;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, (string accessToken, string refreshToken, User)>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<(string , string , User)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var findUser = await _usersRepository.GetByEmail(request.Email);

            if (findUser == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var isValidPassword = _passwordHasher.Verify(request.Password, findUser.PasswordHash);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var (accessToken, refreshToken) = _jwtProvider.Generate(findUser);
            return (accessToken, refreshToken, findUser);
        }
    }

}

