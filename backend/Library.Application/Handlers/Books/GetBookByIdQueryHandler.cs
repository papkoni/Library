using System;
using Library.Application.Cache;
using Library.Application.Queries.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, (Book, byte[])>
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IImageCacheHandler _imageCacheHandler;

        public GetBookByIdQueryHandler(IBooksRepository booksRepository, IImageCacheHandler imageCacheHandler)
        {
            _booksRepository = booksRepository;
            _imageCacheHandler = imageCacheHandler;
        }

        public async Task<(Book, byte[])> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetBookById(request.Id);
            if (book == null)
                throw new KeyNotFoundException($"Book with Id {request.Id} not found.");

            var imageBytes = await _imageCacheHandler.GetImageAsync(book.ImageName);
            if (imageBytes == null)
                throw new InvalidOperationException("Image not found for the book.");

            return (book, imageBytes);
        }
    }

}

