using System;
using System.Threading.Tasks;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IAuthorRepository
	{
        Task<List<Author>> GetAllAuthors();
        Task<Author?> GetAuthorById(Guid id);
        Task AddAuthor(Author author);
        Task DeleteAuthor(Guid id);
        Task<List<Book>> GetBooksByAuthorId(Guid authorId);
        Task UpdateAuthor(Author author);
    }
}

