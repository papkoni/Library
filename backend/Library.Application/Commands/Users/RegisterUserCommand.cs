using System;
using MediatR;

namespace Library.Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<(string AccessToken, string RefreshToken)>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public RegisterUserCommand(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }

}

