using System;
using Library.Core.Models;
using MediatR;

namespace Library.Application.Queries.Authors
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
    }
}

