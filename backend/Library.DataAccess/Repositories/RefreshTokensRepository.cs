using System;
using Library.Core.Abstractions;
using Library.Core.Models;

using Microsoft.EntityFrameworkCore;

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

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateToken(Guid id, string token)
        {
            
            await _context.RefreshTokens
                .Where(rt => rt.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(rt => rt.Token, token)
                .SetProperty(rt => rt.ExpiryDate, DateTime.UtcNow)
                );

        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            var refreshTokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User) // Если нужно подгружать данные пользователя
                .FirstOrDefaultAsync(rt => rt.Token == token);
            return RefreshToken.Create(refreshTokenEntity.Id, refreshTokenEntity.Token, refreshTokenEntity.ExpiryDate);
        }
    }
}

