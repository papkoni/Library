using System;
using Library.Application.Commands.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
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
            // Создаем книгу
            var book = Book.Create(
                id: Guid.NewGuid(),
                title: request.Title,
                isbn: request.ISBN,
                description: request.Description,
                recieveDate: request.RecieveDate,
                returnDate: request.ReturnDate,
                genre: request.Genre,
                authorId: request.AuthorId,
                userId: request.UserId,
                imageName: request.ImageName
            );

            // Добавляем книгу через репозиторий
            await _booksRepository.AddBook(book);

            // Возвращаем результат
            return Unit.Value;
        }
    }


}

