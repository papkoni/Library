using System;
using Library.Core.Models;
using Microsoft.EntityFrameworkCore;
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

            var authors = authorEntities.Select(authorEntity =>
            {
                // Преобразуем книги автора
                var books = authorEntity.Books?.Select(bookEntity =>
                {
                    // Создаем объект Book с помощью метода Create
                    return Book.Create(
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
                }).ToList() ?? new List<Book>();

                return Author.Create(
                    authorEntity.Id,
                    authorEntity.FirstName,
                    authorEntity.Surname,
                    authorEntity.Birthday,
                    authorEntity.Country,
                    books
                );
            }).ToList();

            return authors;
        }

        public async Task<Author?> GetAuthorById(Guid id)
        {
            var author = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            var books = author.Books?.Select(book =>
            {
                return Book.Create(
                    book.Id,
                    book.Title,
                    book.ISBN,
                    book.Description,
                    book.RecieveDate,
                    book.ReturnDate,
                    book.Genre,
                    book.AuthorId,
                    book.UserId,
                    book.ImageName
                );
            }).ToList() ?? new List<Book>();

            // Создаем объект Author
            return Author.Create(
                author.Id,
                author.FirstName,
                author.Surname,
                author.Birthday,
                author.Country,
                books
            );
        }


        public async Task AddAuthor(Author author)
        {
            

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Author author)
        {
            
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
           
        }


        public async Task<List<Book>> GetBooksByAuthorId(Guid authorId)
        {
            var authorEntity = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)  // Подгружаем книги
                .FirstOrDefaultAsync(a => a.Id == authorId);

           

            var books = authorEntity.Books?.Select(bookEntity =>
            {
                return Book.Create(
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
            }).ToList() ?? new List<Book>();

            return books;
        }




        public async Task UpdateAuthor(Author author, CancellationToken cancellationToken)
        {
           

            await _context.Authors
                .Where(a => a.Id == author.Id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.FirstName, author.FirstName)
                    .SetProperty(a => a.Surname, author.Surname)
                    .SetProperty(a => a.Birthday, author.Birthday)
                    .SetProperty(a => a.Country, author.Country),
                    cancellationToken
                );
        }











    }
}

