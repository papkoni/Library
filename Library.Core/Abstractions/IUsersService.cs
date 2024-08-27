using System;
namespace Library.Core.Abstractions
{
	public interface IUsersService
	{
        Task Register(string name, string password, string email);
        Task<string> Login(string email, string password);
    }
}

