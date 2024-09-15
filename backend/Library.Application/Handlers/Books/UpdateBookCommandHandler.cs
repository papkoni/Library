using System;
using Library.Application.Commands.Books;
using Library.Core.Abstractions;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IBooksRepository _booksRepository;

        public UpdateBookCommandHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetBookById(request.Book.Id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {request.Book.Id} not found.");
            }

            await _booksRepository.Update(request.Book);
            return Unit.Value;
        }
    }

}

