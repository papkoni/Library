using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Books
{
    public class GetBookByIdQuery : IRequest<(Book, byte[])>
    {
        public Guid Id { get; }

        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}

