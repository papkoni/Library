using AutoMapper;
using Library.Core.Models;
using Library.Core.Abstractions;

using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Book?>> GetAllBooks()
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
           

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
           

            await _context.Books
                .Where(b => b.Id == book.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, book.Title)
                    .SetProperty(b => b.ISBN, book.ISBN)
                    .SetProperty(b => b.Description, book.Description)
                    .SetProperty(b => b.RecieveDate, book.RecieveDate)
                    .SetProperty(b => b.ReturnDate, book.ReturnDate)
                    .SetProperty(b => b.Genre, book.Genre)
                    .SetProperty(b => b.AuthorId, book.AuthorId)
                    .SetProperty(b => b.ImageName, book.ImageName)
                    .SetProperty(b => b.UserId, book.UserId)
                );
        }

        public async Task Delete(Guid id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
        }






    }
}



