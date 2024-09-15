using System;
using System.Xml.Linq;

namespace Library.Core.Models
{

	public class RefreshToken
	{

        public Guid Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public User? User { get; set; }

        private RefreshToken() { }


        private RefreshToken(Guid id, string token, DateTime expiryDate)
        {
            Id = id;
            Token = token;
            ExpiryDate = expiryDate;
            
        }

        public static RefreshToken Create(Guid id, string token, DateTime expiryDate)
        {
            return new RefreshToken(id, token, expiryDate);
        }
    }
}

