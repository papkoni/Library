using System;
using Library.Application.Queries.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class GetBooksByISBNQueryHandler : IRequestHandler<GetBooksByISBNQuery, Book?>
    {
        private readonly IBooksRepository _booksRepository;

        public GetBooksByISBNQueryHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book?> Handle(GetBooksByISBNQuery request, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetBooksByISBN(request.ISBN);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ISBN {request.ISBN} not found.");
            }
            return book;
        }
    }

}

