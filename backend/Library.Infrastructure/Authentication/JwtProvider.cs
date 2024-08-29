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

        public ClaimsPrincipal? ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_options.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero 
                };

                var principal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                var isRefreshTokenClaim = principal.Claims.FirstOrDefault(c => c.Type == "isRefreshToken");
                if (isRefreshTokenClaim == null || isRefreshTokenClaim.Value != "true")
                {
                    throw new SecurityTokenException("Invalid refresh token.");
                }

                return principal;
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

        public ClaimsPrincipal? ValidateAccessToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_options.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    var userIdClaim = principal.FindFirst("userId");
                    if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                    {
                        throw new SecurityTokenException("Invalid token");
                    }
                }

                return principal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

    }


}

