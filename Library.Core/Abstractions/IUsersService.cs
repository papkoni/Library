using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IUsersService
	{
        Task<(string, string, string, string, Guid)> Register(string name, string password, string email);
        Task<string> Login(string email, string password);
    }
}

