using System;
using Library.API.Contracts;
using Library.Application.Services;
using Library.Core.Abstractions;
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

            // Преобразуем Author в AuthorsResponse
            var response = authors.Select(a => new AuthorsResponse(
                a.Id,
                a.FirstName,
                a.Surname,
                a.Birthday,
                a.Country,
                a.Books?.Select(b => new BookWithotImageResponse(
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
                   
                )).ToList()
            )).ToList();

            return Ok(response);  // Возвращаем список авторов
        }
    }

}

