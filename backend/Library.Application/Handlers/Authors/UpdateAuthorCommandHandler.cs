using System;
using Library.Application.Commands.Authors;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Authors
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorById(request.Id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {request.Id} not found.");
            }

            var updatedAuthor = Author.Create(request.Id, request.FirstName, request.Surname, request.Birthday, request.Country);
            await _authorRepository.UpdateAuthor(updatedAuthor, cancellationToken);

            return Unit.Value;
        }
    }


}

