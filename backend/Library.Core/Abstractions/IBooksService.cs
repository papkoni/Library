using System;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Core.Abstractions
{
	public interface IBooksService
	{
        Task<List<Book>> GetAllBooks();
        Task<(Book, byte[])> GetBookById(Guid id);
        Task<Book?> GetBooksByISBN(string isbn);
        Task<List<Book>> GetByPage(int page, int pageSize);
        Task AddBook(Book book);
        Task Update(Book book);
        Task Delete(Guid id);
    }
}

