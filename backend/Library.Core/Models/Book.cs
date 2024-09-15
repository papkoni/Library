using System;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Library.Core.Models
{

	public class Book
	{
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? RecieveDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string Genre { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }

        public Author? Author { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public string ImageName { get; set; } = string.Empty;

        private Book() { }


        private Book(Guid id, string title, string isbn, string description,
            DateTime? recieveDate, DateTime? returnDate, string genre, Guid authorId, string imageName, Guid? userId)
		{
            Id = id;
            Title = title;
            ISBN = isbn;
            Description = description;
            RecieveDate = recieveDate;
            ReturnDate = returnDate;
            Genre = genre;
            AuthorId = authorId;
            ImageName = imageName;
            UserId = userId;

        }

        


        public static Book Create(Guid id, string title, string isbn, string description,DateTime? recieveDate,
            DateTime? returnDate, string genre, Guid authorId, Guid? userId, string imageName = "")
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var book = new Book(id, title, isbn, description, recieveDate, returnDate, genre, authorId, imageName, userId);

            return book;
        }

       


    }
}

