using System;
namespace Library.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int AccessTokenExpiresMinutes { get; set; } = 15; 
        public int RefreshTokenExpiresDays { get; set; } = 7;
    }

}

