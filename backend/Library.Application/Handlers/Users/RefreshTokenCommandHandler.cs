using System;
using Library.Application.Auth;
using Library.Application.Commands.Users;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Handlers.Users
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, (string, string, User)>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public RefreshTokenCommandHandler(IUsersRepository usersRepository, IJwtProvider jwtProvider, IRefreshTokensRepository refreshTokensRepository)
        {
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<(string, string, User)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var goodToken = _jwtProvider.ValidateRefreshToken(request.OldRefreshToken);
            if (!goodToken)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var oldRefreshTokenInDb = await _refreshTokensRepository.GetRefreshTokenAsync(request.OldRefreshToken);
            if (oldRefreshTokenInDb == null)
            {
                throw new SecurityTokenException("Refresh token not found in database.");
            }

            var user = await _usersRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var (accessToken, refreshToken) = _jwtProvider.Generate(user);
            await _refreshTokensRepository.UpdateToken(user.RefreshTokenId, refreshToken);

            return (accessToken, refreshToken, user);
        }
    }

}

