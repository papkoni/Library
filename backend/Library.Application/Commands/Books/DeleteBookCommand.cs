using System;
using MediatR;

namespace Library.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}

