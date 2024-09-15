using System;
using Library.Application.Queries.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IBooksRepository _booksRepository;

        public GetAllBooksQueryHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepository.GetAllBooks();
        }
    }
}

