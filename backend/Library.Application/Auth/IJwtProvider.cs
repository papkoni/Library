using System;
using System.Security.Claims;
using Library.Core.Models;

namespace Library.Application.Auth
{
    public interface IJwtProvider
    {
        (string accessToken, string refreshToken) Generate(User user);
        bool ValidateRefreshToken(string refreshToken);
    }
}

