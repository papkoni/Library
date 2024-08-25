using System;
using Library.API.Contracts;
using Library.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController: ControllerBase
    {

        private readonly IBooksService _booksService;

        public BookController(IBooksService booksService)
		{
            _booksService = booksService;
        }

        //[HttpGet("AllBooks")]
        //public async Task<ActionResult<List<BooksResponse>>> GetAllBook()
        //{

        //}

    }
}

