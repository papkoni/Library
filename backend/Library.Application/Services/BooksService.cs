
using Library.Core.Abstractions;
using Library.Core.Models;

namespace Library.Application.Services
{
	public class BooksService: IBooksService
    {

		private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
		{
			_booksRepository = booksRepository;

        }

        public async Task<List<Book>> GetAllBooks()
		{
			return await _booksRepository.GetAllBooks();

        }

        public async Task<Book?> GetBooksById(Guid id)
        {
            return await _booksRepository.GetBooksById(id);
        }


        public async Task<Book?> GetBooksByISBN(string isbn)
        {
            return await _booksRepository.GetBooksByISBN(isbn);
        }

        public async Task<List<Book>> GetByPage(int page, int pageSize)
        {
            return await _booksRepository.GetByPage(page, pageSize);
        }

        public async Task AddBook(Book book)
        {
            await _booksRepository.AddBook(book);
        }

        public async Task Update(Book book)
        {
            await _booksRepository.Update(book);
        }

        public async Task Delete(Guid id)
        {
            await _booksRepository.Delete(id);
        }

        

        
    }
}

