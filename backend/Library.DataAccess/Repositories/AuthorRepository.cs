using System;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess.Entites;
using Library.Core.Abstractions;

namespace Library.DataAccess.Repositories
{
	public class AuthorRepository: IAuthorRepository
    {
        private readonly LibraryDbContext _context;


        public AuthorRepository(LibraryDbContext context)
		{
            _context = context;

        }

       public async Task<List<Author>> GetAllAuthors()
        {
            var authorEntities = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books) // Подгружаем книги, если нужно
                .ToListAsync();

            var authors = new List<Author>();

            foreach (var authorEntity in authorEntities)
            {
                var books = authorEntity.Books?.Select(bookEntity => 
                {
                    var bookResult = Book.Create(
                        bookEntity.Id, 
                        bookEntity.Title, 
                        bookEntity.ISBN, 
                        bookEntity.Description, 
                        bookEntity.RecieveDate,
                        bookEntity.ReturnDate, 
                        bookEntity.Genre, 
                        bookEntity.AuthorId, 
                        bookEntity.UserId, 
                        bookEntity.ImageName
                    );

                    // Проверяем успешность создания книги
                    return bookResult.IsSuccess ? bookResult.Value : null;
                }).Where(book => book != null).ToList() ?? new List<Book>();

                // Создаем автора
                var authorResult = Author.Create(
                    authorEntity.Id, 
                    authorEntity.FirstName, 
                    authorEntity.Surname, 
                    authorEntity.Birthday, 
                    authorEntity.Country, 
                    books
                );

                // Проверяем успешность создания автора
                if (authorResult.IsSuccess)
                {
                    authors.Add(authorResult.Value);
                }
            }

            return authors;
}










    }
}

