﻿using System;
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


        private Book(Guid id, string title, string isbn, string description,
            DateTime? recieveDate, DateTime? returnDate, string genre, Guid authorId)
		{
            Id = id;
            Title = title;
            ISBN = isbn;
            Description = description;
            RecieveDate = recieveDate;
            ReturnDate = returnDate;
            Genre = genre;
            AuthorId = authorId;
		}

       


        public static Result<Book> Create(Guid id, string title, string isbn, string description,
            DateTime? recieveDate, DateTime? returnDate, string genre, Guid authorId)
        {
            // ADD VALIDATION!!!!!!!!!!!!!!!

            var book = new Book(id, title, isbn, description, recieveDate, returnDate, genre, authorId);

            return Result.Success(book);
        }

        


    }
}

