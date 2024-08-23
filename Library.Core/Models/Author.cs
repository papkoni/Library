using System;
using CSharpFunctionalExtensions;

namespace Library.Core.Models
{
	public class Author
	{
        public Guid Id { get; }
        public string FirstName { get;  } = string.Empty;
        public string Surname { get;  } = string.Empty;
        public DateTime? Birthday { get;  }
        public string? Country { get; } = string.Empty;


        private Author(Guid id, string firstName, string surname,
            DateTime? birthday, string? country)
		{
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Birthday = birthday;
            Country = country;
        }


        public static Result<Author> Create(Guid id, string firstName, string surname,
            DateTime? birthday, string? country)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var author = new Author(id, firstName, surname, birthday, country);

            return Result.Success(author);
        }
    }
}

