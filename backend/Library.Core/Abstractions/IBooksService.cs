using System;
using CSharpFunctionalExtensions;
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
         
        Task Update(Book book);
        Task Delete(Guid id);
        Task AddBook(Book book);
    }
}

