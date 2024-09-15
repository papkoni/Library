using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Authors
{
    public class GetAuthorByIdQuery : IRequest<Author?>
    {
        public Guid Id { get; }

        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}

