using System;
using Library.API.Contracts;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BooksController: ControllerBase
    {

        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
		{
            _booksService = booksService;
        }

        [HttpGet("AllBooks"), Authorize]

        public async Task<ActionResult<List<BooksResponse>>> GetAllBooks()
        {
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.ISBN, b.Description,
                b.RecieveDate, b.ReturnDate, b.Genre, b.AuthorId));
            return Ok(response);
        }

        [HttpPost("AddBook")]

        public async Task<ActionResult> AddBook([FromBody] BooksRequest booksRequest)
        {
            var book = Book.Create(Guid.NewGuid(), booksRequest.title, booksRequest.isbn, booksRequest.description,
                booksRequest.recieveDate, booksRequest.returnDate, booksRequest.genre, Guid.Parse(booksRequest.authorID));

            await _booksService.AddBook(book);
            return Ok();

        }



    }
}

