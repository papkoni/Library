using AutoMapper;
using Library.Core.Models;
using Library.Core.Abstractions;

using Library.DataAccess.Mapper.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories
{
    public class BooksRepository: IBooksRepository
    {
        private readonly LibraryDbContext _context;

        private readonly IMapper _mapper;


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


            return bookEntities
                .Select(bookEntity => bookEntity.MapToModel(_mapper))
                .ToList();

        }


        public async Task<Book?> GetBooksById(Guid id)
        {

            var bookEntitiy = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return bookEntitiy.MapToModel(_mapper);

        }

        public async Task<Book?> GetBooksByISBN(string isbn)
        {

            var bookEntitiy = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);

            return bookEntitiy.MapToModel(_mapper);

        }

        public async Task<List<Book>> GetByPage(int page, int pageSize)
        {

            var bookEntities = await _context.Books
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return bookEntities
                .Select(bookEntity => bookEntity.MapToModel(_mapper))
                .ToList();

        }

        public async Task AddBook(Book book)
        {
            var bookEntity = book.MapToEntity(_mapper);

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            var bookEntity = book.MapToEntity(_mapper);

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



