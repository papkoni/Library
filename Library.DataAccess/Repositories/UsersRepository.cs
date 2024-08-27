﻿using AutoMapper;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
	public class UsersRepository: IUsersRepository
    {

        private readonly LibraryDbContext _context;

        private readonly IMapper _mapper;

        public UsersRepository(LibraryDbContext context, IMapper mapper)
		{
            _context = context;

            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            //var userEntity = new UserEntity()
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    PasswordHash = user.PasswordHash,
            //    Email = user.Email
            //};

            //await _context.Users.AddAsync(userEntity);
            //await _context.SaveChangesAsync();
        }


        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new Exception();

            return User.Create(userEntity.Id, userEntity.Name, userEntity.PasswordHash, userEntity.Email);
        }

    }
}

