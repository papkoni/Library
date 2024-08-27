using System;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.DataAccess.Entites;

namespace Library.DataAccess.Repositories
{
	public class RefreshTokensRepository: IRefreshTokensRepository
    {
        private readonly LibraryDbContext _context;


        public RefreshTokensRepository(LibraryDbContext context)
		{
            _context = context;

        }


        public async Task Add(RefreshToken refreshToken)
        {
            var refreshTokenEntity = new RefreshTokenEntity()
            {
                Id = refreshToken.Id,
                Token = refreshToken.Token,
                ExpiryDate = refreshToken.ExpiryDate

            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();
        }

    }
}

