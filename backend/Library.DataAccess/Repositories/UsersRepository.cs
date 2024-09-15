using AutoMapper;
using Library.Core.Abstractions;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
	public class UsersRepository: IUsersRepository
    {
        

        private readonly LibraryDbContext _context;


        public UsersRepository(LibraryDbContext context)
		{
            _context = context;

        }

        //ПЕРЕДЕЛАТЬ ВСЕ ПОД НОВЫЕ ЭНТИТИ
        public async Task Add(User user)
        {
           

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        

        public async Task<User?> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (userEntity == null)
            {
                return null;
            }

            var user = User.Create(userEntity.Id, userEntity.Name, userEntity.PasswordHash, userEntity.Email, userEntity.Role);
            user.RefreshTokenId = userEntity.RefreshTokenId;
            return user;
        }

        public async Task<User?> GetById(string id)
        {
            
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));

            if (userEntity == null)
            {
                return null;
            }

            var user = User.Create(userEntity.Id, userEntity.Name, userEntity.PasswordHash, userEntity.Email, userEntity.Role);
            user.RefreshTokenId = userEntity.RefreshTokenId;
            return user;
        }


    }
}

