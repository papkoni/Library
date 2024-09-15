
namespace Library.Core.Models
{
	public class Author
	{
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }

        public string? Country { get; set; } = string.Empty;

        public List<Book>? Books { get; set; }  // Change this to a collection

        private Author() { }


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

        private Author(Guid id, string firstName, string surname,
            DateTime? birthday, string? country)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            Birthday = birthday;
            Country = country;
           
        }

        public static Author Create(Guid id, string firstName, string surname,
            DateTime? birthday, string? country, List<Book>? books)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var author = new Author(id, firstName, surname, birthday, country, books);

            return author;
        }

        public static Author Create(Guid id, string firstName, string surname,
           DateTime? birthday, string? country)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var author = new Author(id, firstName, surname, birthday, country);

            return author;
        }

    }
}

