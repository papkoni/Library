using System;
using Library.Application.Commands.Authors;
using Library.Core.Abstractions;
using MediatR;

namespace Library.Application.Handlers.Authors
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorById(request.Id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {request.Id} not found.");
            }

            await _authorRepository.DeleteAuthor(author);
            return Unit.Value;
        }
    }


}

