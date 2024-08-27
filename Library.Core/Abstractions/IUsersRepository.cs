using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IUsersRepository
	{
        Task Add(User user);

        Task<User?> GetByEmail(string email);
    }
}

