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
            // Получаем всех авторов с подгруженными книгами
            var authorEntities = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books) // Подгружаем книги, если нужно
                .ToListAsync();

            // Преобразуем authorEntities в Author с помощью LINQ
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

                // Создаем объект Author
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
            // Получаем автора с подгруженными книгами
            var authorEntity = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (authorEntity == null)
            {
                return null; // Если автора не найдено
            }

            // Преобразуем книги автора
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

            // Создаем объект Author
            return Author.Create(
                authorEntity.Id,
                authorEntity.FirstName,
                authorEntity.Surname,
                authorEntity.Birthday,
                authorEntity.Country,
                books
            );
        }


        public async Task AddAuthor(Author author)
        {
            var authorEntity = new AuthorEntity
            {
                Id = author.Id,
                FirstName = author.FirstName,
                Surname = author.Surname,
                Birthday = author.Birthday,
                Country = author.Country
            };

            await _context.Authors.AddAsync(authorEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Guid id)
        {
            var authorEntity = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (authorEntity != null)
            {
                _context.Authors.Remove(authorEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Author not found.");
            }
        }


        public async Task<List<Book>> GetBooksByAuthorId(Guid authorId)
        {
            var authorEntity = await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)  // Подгружаем книги
                .FirstOrDefaultAsync(a => a.Id == authorId);

            if (authorEntity == null)
            {
                throw new Exception("Author not found.");
            }

            // Преобразуем книги автора
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




        public async Task UpdateAuthor(Author author)
        {
            // Создаем временный объект с обновленными данными
            var authorEntity = new AuthorEntity
            {
                Id = author.Id,
                FirstName = author.FirstName,
                Surname = author.Surname,
                Birthday = author.Birthday,
                Country = author.Country
            };

            // Выполняем обновление записи в базе данных
            await _context.Authors
                .Where(a => a.Id == authorEntity.Id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.FirstName, authorEntity.FirstName)
                    .SetProperty(a => a.Surname, authorEntity.Surname)
                    .SetProperty(a => a.Birthday, authorEntity.Birthday)
                    .SetProperty(a => a.Country, authorEntity.Country)
                );
        }











    }
}

