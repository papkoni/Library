
using Library.Application.Cache;
using Library.Core.Abstractions;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Services
{
	public class BooksService: IBooksService
    {


		private readonly IBooksRepository _booksRepository;

        private readonly IImageCacheHandler _imageCacheHandler;

        public BooksService(IBooksRepository booksRepository, IImageCacheHandler imageCacheHandler)
		{
			_booksRepository = booksRepository;
            _imageCacheHandler = imageCacheHandler;
        }

        public async Task<List<Book>> GetAllBooks()
		{
			return await _booksRepository.GetAllBooks();

        }

        public async Task<(Book, byte[])> GetBookById(Guid id)
        {
            var book =  await _booksRepository.GetBookById(id);

            if(book == null)
            {
                throw new Exception("GetBooksById in service return null");
            }
            var imageBytes = await _imageCacheHandler.GetImageAsync(book.ImageName);

            if (imageBytes == null)
            {
                throw new Exception("GetBooksById in return null");
            }
            return (book, imageBytes);

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

