using System;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

namespace Library.Core.Models
{

	public class Book
	{
        public Guid Id { get; }


        public string Title { get; } = string.Empty;

        public string ISBN { get;  } = string.Empty;

        public string Description { get;set; } = string.Empty;

        public DateTime? RecieveDate { get; }

        public DateTime? ReturnDate { get;  }

        public string Genre { get; } = string.Empty;

        public Guid Author { get; }

        public string ImageName { get; set; } = string.Empty;

        public Guid? User { get; set; }


       
        private Book(Guid id, string title, string isbn, string description,
            DateTime? recieveDate, DateTime? returnDate, string genre, Guid author,string imageName, Guid? user)
		{
            Id = id;
            Title = title;
            ISBN = isbn;
            Description = description;
            RecieveDate = recieveDate;
            ReturnDate = returnDate;
            Genre = genre;
            Author = author;
            ImageName = imageName;
            User = user;

        }

        


        public static Book Create(Guid id, string title, string isbn, string description,DateTime? recieveDate,
            DateTime? returnDate, string genre, Guid author, Guid? user, string imageName = "")
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var book = new Book(id, title, isbn, description, recieveDate, returnDate, genre, author, imageName, user);

            return book;
        }

       


    }
}

