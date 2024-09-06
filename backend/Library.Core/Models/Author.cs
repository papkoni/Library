using System;
using CSharpFunctionalExtensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Core.Models
{
	public class Author
	{
        public Guid Id { get;  }

        public string FirstName { get; } = string.Empty;

        public string Surname { get; } = string.Empty;

        public DateTime? Birthday { get; }

        public string? Country { get;  } = string.Empty;

        public List<Book>? Books { get; }  // Change this to a collection

        private Author(Guid id, string firstName, string surname,
            DateTime? birthday, string? country, List<Book>? books )
		{
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Birthday = birthday;
            Country = country;
            Books = books;
        }


        public static Result<Author> Create(Guid id, string firstName, string surname,
            DateTime? birthday, string? country, List<Book>? books)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var author = new Author(id, firstName, surname, birthday, country, books);

            return Result.Success(author);
        }
    }
}

