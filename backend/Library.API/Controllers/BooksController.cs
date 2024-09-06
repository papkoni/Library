using System;
using Library.API.Contracts;
using Library.Application.Image;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.DataAccess.Repositories;
using Library.Infrastructure.WorkWithImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BooksController: ControllerBase
    {

        private readonly IBooksService _booksService;
        private readonly IUpload _upload;

        public BooksController(IBooksService booksService, IUpload upload)
		{
            _booksService = booksService;
            _upload = upload;
        }

        [HttpGet("AllBooks")]
        
        public async Task<ActionResult<List<BooksResponse>>> GetAllBooks()
        {
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.ISBN, b.Description,
                b.RecieveDate, b.ReturnDate, b.Genre, b.Author));
            return Ok(response); // return response
        }

        [HttpPost("AddBook")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddBook([FromBody] BookRequest bookRequest)
        {
            var imageName = await _upload.UploadImage(bookRequest.imageFile, bookRequest.imageName);

            if (string.IsNullOrEmpty(imageName))
            {
                imageName = " ";
            }

            var book = Book.Create(Guid.NewGuid(), bookRequest.title, bookRequest.isbn, bookRequest.description,
                bookRequest.recieveDate, bookRequest.returnDate, bookRequest.genre, bookRequest.author, bookRequest.user, imageName);

             await _booksService.AddBook(book);

            return Ok();
        }


        [HttpGet("BookById")]
        public async Task<ActionResult> GetBooksById(Guid id)
        {
            var (book, image) = await _booksService.GetBookById(id);



            var response = new BookResponse(
                id: book.Id,
                title: book.Title,
                isbn: book.ISBN,
                description: book.Description,
                recieveDate: book.RecieveDate,
                returnDate: book.ReturnDate,
                genre: book.Genre,
                author: book.Author,
                user: book.User,
                imageByte: image,
                imageName: book.ImageName
            );


            return Ok(response);

        }

        //public async Task<ActionResult> AddBook(Book book)
        //{
        //    await _upload.UploadImage();
        //    await _booksService.AddBook(book);
        //    return Ok();
        //}

        //public async Task Update(Book book)
        //{
        //    await _booksRepository.Update(book);
        //}

        //public async Task Delete(Guid id)
        //{
        //    await _booksRepository.Delete(id);
        //}

    }
}

