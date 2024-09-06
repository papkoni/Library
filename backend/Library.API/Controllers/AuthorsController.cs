using System;
using System.Linq;
using Library.API.Contracts;
using Library.Application.Services;
using Library.Core.Abstractions;
using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AuthorsResponse>>> GetAllAuthors()
        {
            var authors = await _authorsService.GetAllAuthors();

            var response = authors.Select(a => new AuthorsResponse(
                a.Id,
                a.FirstName,
                a.Surname,
                a.Birthday,
                a.Country,
                a.Books?.Select(b => new BookWithoutImageResponse(
                    b.Id,
                    b.Title,
                    b.ISBN,
                    b.Description,
                    b.RecieveDate,
                    b.ReturnDate,
                    b.Genre,
                    b.Author,
                    b.User,
                    b.ImageName
                )).ToList() // Список книг может быть null
            )).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] AuthorRequest authorRequest)
        {
            var books = authorRequest.books?.Select(b => Book.Create(
               b.id,
                b.title,
                b.isbn,
                b.description,
                b.recieveDate,
                b.returnDate,
                b.genre,
                b.author,
                b.user,
                b.imageName
            )).ToList();

            var author = Author.Create(
                Guid.NewGuid(),
                authorRequest.firstName,
                authorRequest.surname,
                authorRequest.birthday,
                authorRequest.country,
                books // books может быть null
            );

            try
            {
                await _authorsService.AddAuthor(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(Guid id, [FromBody] AuthorRequest authorRequest)
        {
            var books = authorRequest.books?.Select(b => Book.Create(
                b.id,
                b.title,
                b.isbn,
                b.description,
                b.recieveDate,
                b.returnDate,
                b.genre,
                b.author,
                b.user,
                b.imageName
            )).ToList();

            var author = Author.Create(
                id,
                authorRequest.firstName,
                authorRequest.surname,
                authorRequest.birthday,
                authorRequest.country,
                books // books может быть null
            );

            try
            {
                await _authorsService.UpdateAuthor(author);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                await _authorsService.DeleteAuthor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<List<BookWithoutImageResponse>>> GetBooksByAuthor(Guid id)
        {
            var books = await _authorsService.GetBooksByAuthor(id);

            var response = books?.Select(b => new BookWithoutImageResponse(
                b.Id,
                b.Title,
                b.ISBN,
                b.Description,
                b.RecieveDate,
                b.ReturnDate,
                b.Genre,
                b.Author,
                b.User,
                b.ImageName
            )).ToList() ?? new List<BookWithoutImageResponse>();

            return Ok(response);
        }
    }



}

