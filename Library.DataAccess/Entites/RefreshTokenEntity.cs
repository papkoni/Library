using System;
using Library.Core.Models;

namespace Library.DataAccess.Entites
{

	public class RefreshTokenEntity
	{
        public Guid Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public required UserEntity User { get; set; } 

    }


}

