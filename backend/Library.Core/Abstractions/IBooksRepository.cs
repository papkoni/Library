using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IBooksRepository
	{
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBooksById(Guid id);
        Task<Book?> GetBooksByISBN(string isbn);
        Task<List<Book>> GetByPage(int page, int pageSize);
        Task AddBook(Book book);
        Task Update(Book book);
        Task Delete(Guid id);

    }
}

