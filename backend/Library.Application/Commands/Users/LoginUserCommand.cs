using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<(string AccessToken, string RefreshToken, User User)>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

}

