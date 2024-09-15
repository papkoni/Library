using System;
using Library.API.Contracts;
using Library.Application.Commands.Books;
using Library.Application.Image;
using Library.Application.Queries.Books;
using Library.Core.Abstractions;
using Library.Core.Models;
using Library.DataAccess.Repositories;
using Library.Infrastructure.WorkWithImage;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUpload _upload;

        public BooksController(IMediator mediator, IUpload upload)
        {
            _mediator = mediator;
            _upload = upload;
        }

        [HttpGet("AllBooks")]
        public async Task<ActionResult<List<BooksResponse>>> GetAllBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());

            var response = books.Adapt<List<BooksResponse>>();

            return Ok(response);
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            var imageName = await _upload.UploadImage(bookRequest.imageByte, bookRequest.imageName);
            bookRequest = bookRequest with { imageName = imageName ?? " " };

            var command = bookRequest.Adapt<AddBookCommand>();

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("BookById")]
        public async Task<ActionResult<BookResponse>> GetBooksById(Guid id)
        {
            var (book, image) = await _mediator.Send(new GetBookByIdQuery(id));

            var response = book.Adapt<BookResponse>() with { imageByte = image };

            return Ok(response);
        }

        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            await _mediator.Send(new DeleteBookCommand(id));
            return Ok();
        }

        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook([FromBody] BookRequest bookRequest)
        {
            var command = bookRequest.Adapt<UpdateBookCommand>();

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("BookByISBN")]
        public async Task<ActionResult<BookResponse>> GetBooksByISBN(string isbn)
        {
            var book = await _mediator.Send(new GetBooksByISBNQuery(isbn));

            if (book == null)
                return NotFound();

            var response = book.Adapt<BookResponse>();

            return Ok(response);
        }

        [HttpGet("BooksByPage")]
        public async Task<ActionResult<List<BooksResponse>>> GetBooksByPage(int page, int pageSize)
        {
            var books = await _mediator.Send(new GetBooksByPageQuery(page, pageSize));

            // Маппинг книг на BooksResponse
            var response = books.Adapt<List<BooksResponse>>();

            return Ok(response);
        }
    }


}

