using System;
using System.Linq;
using Library.API.Contracts;
using Library.Application.Commands.Authors;
using Library.Application.Queries.Authors;
using Library.Core.Abstractions;
using Library.Core.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AuthorsResponse>>> GetAllAuthors()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());

            var response = authors.Adapt<List<AuthorsResponse>>();

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddAuthor([FromBody] AuthorRequest authorRequest)
        {
            try
            {
                var command = authorRequest.Adapt<AddAuthorCommand>();

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateAuthor( [FromBody] UpdateAuthorRequest updateAuthorRequest)
        {
            try
            {
                var command = updateAuthorRequest.Adapt<UpdateAuthorCommand>();

                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteAuthorCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }


}

