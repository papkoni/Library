using System;
using Library.Application.Queries.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class GetBooksByPageQueryHandler : IRequestHandler<GetBooksByPageQuery, List<Book>>
    {
        private readonly IBooksRepository _booksRepository;

        public GetBooksByPageQueryHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> Handle(GetBooksByPageQuery request, CancellationToken cancellationToken)
        {
            return await _booksRepository.GetByPage(request.Page, request.PageSize);
        }
    }
}

