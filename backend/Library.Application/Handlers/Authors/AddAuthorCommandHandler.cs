using System;
using System.Diagnostics.Metrics;
using Library.Application.Commands.Authors;
using Library.Core.Abstractions;
using Library.Core.Models;
using MapsterMapper;
using MediatR;


namespace Library.Application.Handlers.Authors
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Unit>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            //var author = _mapper.Map<Author>(request); ;
            var author =  Author.Create(Guid.NewGuid(), request.FirstName, request.Surname, request.Birthday, request.Country);
            await _authorRepository.AddAuthor(author);

            return Unit.Value; 
        }
    }

}

