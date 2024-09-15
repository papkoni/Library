using System;
using Library.Application.Queries.Users;
using Library.Core.Abstractions;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Handlers.Users
{
    public class CheckAccessQueryHandler : IRequestHandler<CheckAccessQuery, User>
    {
        private readonly IUsersRepository _usersRepository;

        public CheckAccessQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> Handle(CheckAccessQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return user;
        }
    }

}

