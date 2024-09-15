using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public Book Book { get; }

        public UpdateBookCommand(Book book)
        {
            Book = book;
        }
    }
}

