using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Core.Models;
using Library.DataAccess;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Library.Application.Auth;

namespace Library.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public (string accessToken, string refreshToken) Generate(User user)
        {
            // Генерация Access токена
            var accessToken = GenerateToken(user, _options.AccessTokenExpiresMinutes);

            // Генерация Refresh токена
            var refreshToken = GenerateToken(user, _options.RefreshTokenExpiresDays, isRefreshToken: true);

            return (accessToken, refreshToken);
        }

        private string GenerateToken(User user, double expiresIn, bool isRefreshToken = false)
        {
            Claim[] claims = new Claim[]
            {
            new Claim("userId", user.Id.ToString())
            };

            if (isRefreshToken)
            {
                claims = new Claim[]
                {
                new Claim("userId", user.Id.ToString()),
                new Claim("isRefreshToken", "true")
                };
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public Task UpdateRefreshToken(User user, RefreshToken refreshToken)
        //{

        //}

    }


}

