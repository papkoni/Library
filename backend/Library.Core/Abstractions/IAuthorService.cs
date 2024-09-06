using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IAuthorsService
	{

        Task<List<Author>> GetAllAuthors();
        Task<Author?> GetAuthorById(Guid id);
        Task AddAuthor(Author author);
        Task UpdateAuthor(Author author);
        Task DeleteAuthor(Guid id);
        Task<List<Book>> GetBooksByAuthor(Guid authorId);
    }
}

