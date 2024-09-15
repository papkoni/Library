using System;
using Library.Application.Auth;
using Library.Application.Commands.Users;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, (string, string)>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public RegisterUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IRefreshTokensRepository refreshTokensRepository)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<(string, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var findUser = await _usersRepository.GetByEmail(request.Email);
            if (findUser != null)
            {
                throw new InvalidOperationException("User with the same email already exists.");
            }

            var hashedPassword = _passwordHasher.Generate(request.Password);
            var user = User.Create(Guid.NewGuid(), request.Name, hashedPassword, request.Email, "User");

            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            var token = RefreshToken.Create(Guid.NewGuid(), refreshToken, DateTime.Now);
            user.RefreshTokenId = token.Id;

            await _refreshTokensRepository.Add(token);
            await _usersRepository.Add(user);

            return (accessToken, refreshToken);
        }
    }


}

