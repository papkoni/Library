using System;
using Library.Application.Cache;
using Library.Core.Abstractions;
using Library.Core.Models;

namespace Library.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAuthors();
        }

        public async Task<Author?> GetAuthorById(Guid id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        public async Task AddAuthor(Author author)
        {
            // Перед добавлением можно добавить валидацию
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author), "Author cannot be null.");
            }

            await _authorRepository.AddAuthor(author);
        }

        public async Task UpdateAuthor(Author author)
        {
            // Перед обновлением можно добавить валидацию
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author), "Author cannot be null.");
            }

            await _authorRepository.UpdateAuthor(author);
        }

        public async Task DeleteAuthor(Guid id)
        {
            await _authorRepository.DeleteAuthor(id);
        }

        public async Task<List<Book>> GetBooksByAuthor(Guid authorId)
        {
            return await _authorRepository.GetBooksByAuthorId(authorId);
        }
    }

}

