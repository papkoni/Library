using System;
using Library.Application.Commands.Books;
using Library.Core.Abstractions;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBooksRepository _booksRepository;

        public DeleteBookCommandHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _booksRepository.GetBookById(request.Id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {request.Id} not found.");
            }

            await _booksRepository.Delete(request.Id);
            return Unit.Value;
        }
    }

}

