using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Commands.Authors
{
    public class AddAuthorCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Country { get; set; }
       

        public AddAuthorCommand(string firstName, string surname, DateTime? birthday, string country)
        {
            FirstName = firstName;
            Surname = surname;
            Birthday = birthday;
            Country = country;
        }
    }
}

