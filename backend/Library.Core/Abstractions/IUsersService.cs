using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IUsersService
    {
        Task<(string, string)> Register(string name, string password, string email);
        Task<(string, string, User?)> Login(string email, string password);
        Task<(string, string, User)> Refresh(string oldRefreshToken, string userId);
        Task<User> CheckAccess(string userId);
    }
}

