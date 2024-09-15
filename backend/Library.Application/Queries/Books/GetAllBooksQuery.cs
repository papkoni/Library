using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Books
{
    public class GetAllBooksQuery : IRequest<List<Book>> { }

}

