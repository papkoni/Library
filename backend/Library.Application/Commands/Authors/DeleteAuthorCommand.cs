using System;
using MediatR;

namespace Library.Application.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}

