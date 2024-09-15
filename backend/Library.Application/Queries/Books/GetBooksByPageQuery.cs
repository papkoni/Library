using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Books
{
    public class GetBooksByPageQuery : IRequest<List<Book>>
    {
        public int Page { get; }
        public int PageSize { get; }

        public GetBooksByPageQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}

