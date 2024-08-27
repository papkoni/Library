using System;
using Library.Core.Models;

namespace Library.Application.Auth
{
    public interface IJwtProvider
    {
        (string accessToken, string refreshToken) Generate(User user);
        string GenerateToken(User user, double expiresIn, bool isRefreshToken = false);
    }
}

