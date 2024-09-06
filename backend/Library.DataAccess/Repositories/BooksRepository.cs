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

            // Преобразуем список сущностей в список объектов Book
            var books = bookEntities.Select(bookEntity => Book.Create(
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
            )).ToList();

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

            // Создаем объект Book напрямую, без Result
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

            // Создаем объект Book напрямую, без Result
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
        }

        public async Task<List<Book>> GetByPage(int page, int pageSize)
        {
            var bookEntities = await _context.Books
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Преобразуем сущности книг в объекты Book
            var books = bookEntities.Select(bookEntity => Book.Create(
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
            )).ToList();

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
                    .SetProperty(b => b.Title, bookEntity.Title)
                    .SetProperty(b => b.ISBN, bookEntity.ISBN)
                    .SetProperty(b => b.Description, bookEntity.Description)
                    .SetProperty(b => b.RecieveDate, bookEntity.RecieveDate)
                    .SetProperty(b => b.ReturnDate, bookEntity.ReturnDate)
                    .SetProperty(b => b.Genre, bookEntity.Genre)
                    .SetProperty(b => b.AuthorId, bookEntity.AuthorId)
                    .SetProperty(b => b.ImageName, bookEntity.ImageName)
                    .SetProperty(b => b.UserId, bookEntity.UserId)
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



