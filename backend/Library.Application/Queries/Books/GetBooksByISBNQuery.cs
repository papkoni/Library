using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Books
{
    public class GetBooksByISBNQuery : IRequest<Book?>
    {
        public string ISBN { get; }

        public GetBooksByISBNQuery(string isbn)
        {
            ISBN = isbn;
        }
    }
}

