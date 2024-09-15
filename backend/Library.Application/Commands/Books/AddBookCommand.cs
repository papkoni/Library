using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Commands.Books
{
    public class AddBookCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public DateTime? RecieveDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Genre { get; set; }
        public Guid AuthorId { get; set; }
        public Guid? UserId { get; set; }
        public string ImageName { get; set; }

        public AddBookCommand(string title, string isbn, string description, DateTime? recieveDate,
                              DateTime? returnDate, string genre, Guid authorId, Guid? userId, string imageName)
        {
            Title = title;
            ISBN = isbn;
            Description = description;
            RecieveDate = recieveDate;
            ReturnDate = returnDate;
            Genre = genre;
            AuthorId = authorId;
            UserId = userId;
            ImageName = imageName;
        }
    }

}

