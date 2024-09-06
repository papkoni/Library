using AutoMapper;
using Library.Core.Models;
using Library.Core.Abstractions;

using Library.DataAccess.Mapper.Extensions;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess.Entites;
using System.Linq;

namespace Library.DataAccess.Repositories
{
    public class BooksRepository: IBooksRepository
    {
        private readonly LibraryDbContext _context;

        private readonly IMapper _mapper;

        //ПЕРЕДЕЛАТЬ ВСЕ ПОД НОВЫЕ ЭНТИТИ
        public BooksRepository(LibraryDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;

        }

        public async Task<List<Book>> GetAllBooks()
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .ToListAsync();

            var books = new List<Book>();

            foreach (var bookEntity in bookEntities)
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
                if (bookResult.IsSuccess)
                {
                    books.Add(bookResult.Value);
                }
                else
                {
                    // Логируем или обрабатываем ошибки создания книги
                    Console.WriteLine($"Error creating book: {bookResult.Error}");
                }
            }

            return books;
        }



        public async Task<Book?> GetBookById(Guid id)
        {
            var bookEntity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity == null)
            {
                throw new Exception("GetBookById: book not found.");
            }

            // Создаем книгу с проверкой результата
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

            if (bookResult.IsSuccess)
            {
                return bookResult.Value; // Возвращаем созданную книгу
            }
            else
            {
                // Логируем ошибку создания книги
                Console.WriteLine($"Error creating book: {bookResult.Error}");
                return null;
            }
        }









        public async Task<Book?> GetBooksByISBN(string isbn)
        {
            var bookEntity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);

            if (bookEntity == null)
            {
                throw new Exception("GetBooksByISBN: book not found.");
            }

            // Создаем книгу с проверкой результата
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

            if (bookResult.IsSuccess)
            {
                return bookResult.Value; // Возвращаем созданную книгу
            }
            else
            {
                // Логируем ошибку создания книги
                Console.WriteLine($"Error creating book: {bookResult.Error}");
                return null;
            }
        }


        public async Task<List<Book>> GetByPage(int page, int pageSize)
        {

            var bookEntities = await _context.Books
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var books = new List<Book>();

            foreach (var bookEntity in bookEntities)
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
                if (bookResult.IsSuccess)
                {
                    books.Add(bookResult.Value);
                }
                else
                {
                    // Логируем или обрабатываем ошибки создания книги
                    Console.WriteLine($"Error creating book: {bookResult.Error}");
                }
            }

            return books;

        }

        public async Task AddBook(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Description = book.Description,
                RecieveDate = book.RecieveDate,
                ReturnDate = book.ReturnDate,
                Genre = book.Genre,
                AuthorId = book.Author,
                ImageName = book.ImageName,
                UserId = book.User,


            };
            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                Description = book.Description,
                RecieveDate = book.RecieveDate,
                ReturnDate = book.ReturnDate,
                Genre = book.Genre,
                AuthorId = book.Author,
                ImageName = book.ImageName,
                UserId = book.User,


            };

            await _context.Books
                .Where(b => b.Id == bookEntity.Id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Id, bookEntity.Id)
                .SetProperty(b => b.Title, bookEntity.Title)
                .SetProperty(b => b.ISBN, bookEntity.ISBN)
                .SetProperty(b => b.Description, bookEntity.Description)
                .SetProperty(b => b.RecieveDate, bookEntity.RecieveDate)
                .SetProperty(b => b.ReturnDate, bookEntity.ReturnDate)
                .SetProperty(b => b.Genre, bookEntity.Genre)
                .SetProperty(b => b.AuthorId, bookEntity.AuthorId)

                );

        }


        public async Task Delete(Guid id)
        {

            await _context.Books
               .Where(b => b.Id == id)
               .ExecuteDeleteAsync();
        }

        //ВЫДАЧА НА РУКИ + КАРТИНКА


    }
}



