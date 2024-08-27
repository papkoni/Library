using System;
using CSharpFunctionalExtensions;

namespace Library.Core.Models
{
	public class Book
	{
        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string ISBN { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public DateTime? RecieveDate { get; }

        public DateTime? ReturnDate { get;  }

        public string Genre { get; } = string.Empty;

        public Guid AuthorId { get; }

        public string ImageName { get; set; } = string.Empty;

        public Guid? UserId { get; set; }

        private Book(Guid id, string title, string isbn, string description,
            DateTime? recieveDate, DateTime? returnDate, string genre, Guid authorId,string imageName, Guid? userId)
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
            DateTime? returnDate, string genre, Guid authorId, string imageName = "", Guid? userId = null)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var book = new Book(id, title, isbn, description, recieveDate, returnDate, genre, authorId, imageName, userId);

            return book;
        }

       


    }
}

