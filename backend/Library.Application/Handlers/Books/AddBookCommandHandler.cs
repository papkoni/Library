using System;
using Library.Application.Commands.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using Mapster;
using MediatR;

namespace Library.Application.Handlers.Books
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Unit>
    {
        private readonly IBooksRepository _booksRepository;

        public AddBookCommandHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = request.Adapt<Book>();


            // Добавляем книгу через репозиторий
            await _booksRepository.AddBook(book);

            // Возвращаем результат
            return Unit.Value;
        }
    }


}

