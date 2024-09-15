using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Commands.Users
{
    public class RefreshTokenCommand : IRequest<(string AccessToken, string RefreshToken, User User)>
    {
        public string OldRefreshToken { get; set; }
        public string UserId { get; set; }

        public RefreshTokenCommand(string oldRefreshToken, string userId)
        {
            OldRefreshToken = oldRefreshToken;
            UserId = userId;
        }
    }

}

