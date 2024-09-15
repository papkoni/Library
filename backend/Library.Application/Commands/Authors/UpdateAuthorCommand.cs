using System;
using MediatR;

namespace Library.Application.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public DateTime? Birthday { get; }
        public string? Country { get; }


        public UpdateAuthorCommand(Guid id, string firstName, string surname, DateTime? birthday, string? country)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Birthday = birthday;
            Country = country;
        }
    }

}

