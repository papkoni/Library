using System.Security.Claims;
using Library.API.Contracts;
using Library.Application.Commands.Users;
using Library.Application.Queries.Users;
using Library.Core.Abstractions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var context = HttpContext;

            var command = request.Adapt<RegisterUserCommand>(); // Mapster маппинг
            var (accessToken, refreshToken) = await _mediator.Send(command);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var context = HttpContext;

            var command = request.Adapt<LoginUserCommand>(); // Mapster маппинг
            var (accessToken, refreshToken, user) = await _mediator.Send(command);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken, user });
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh()
        {
            var context = HttpContext;
            var refreshTokenFromCookies = context.Request.Cookies["secretCookie"];

            if (string.IsNullOrEmpty(refreshTokenFromCookies))
            {
                return Unauthorized(new { message = "Refresh token is missing or invalid." });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var command = new RefreshTokenCommand(refreshTokenFromCookies, userIdClaim);

            var (accessToken, refreshToken, user) = await _mediator.Send(command);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken, user });
        }

        [Authorize]
        [HttpGet("CheckAccess")]
        public async Task<IActionResult> CheckAccess()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var query = new CheckAccessQuery(userIdClaim);
                var user = await _mediator.Send(query);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "yyyyy");
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("secretCookie");
            return Ok();
        }
    }

}

