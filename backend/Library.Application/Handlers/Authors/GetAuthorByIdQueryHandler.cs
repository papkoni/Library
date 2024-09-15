using System;
using Library.Application.Queries.Authors;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Authors
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author?>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorById(request.Id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {request.Id} not found.");
            }

            return author;
        }
    }


}

