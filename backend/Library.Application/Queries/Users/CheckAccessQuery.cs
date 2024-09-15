using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Users
{
    public class CheckAccessQuery : IRequest<User>
    {
        public string UserId { get; set; }

        public CheckAccessQuery(string userId)
        {
            UserId = userId;
        }
    }

}

