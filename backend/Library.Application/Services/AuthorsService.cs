using System;
using Library.Application.Cache;
using Library.Core.Abstractions;
using Library.Core.Models;

namespace Library.Application.Services
{
    public class AuthorsService : IAuthorsService
    {


        private readonly IAuthorRepository _authorRepository;


        public AuthorsService(IAuthorRepository booksRepository)
        {
            _authorRepository = booksRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAuthors();

        }
    }
    }

