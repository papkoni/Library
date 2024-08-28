using Library.API.Contracts;
using Library.Application.Services;
using Library.Core.Abstractions;
using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {

        private readonly IUsersService _usersService;


        public UsersController( IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpPost("Register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            var context = HttpContext;

            var (accessToken, refreshToken, name, email, id) = await _usersService.Register(request.Name, request.Password, request.Email);

            var registerUserResponse = new RegisterUserResponse(id, name, email);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Results.Ok(new { accessToken, refreshToken, user = registerUserResponse });
        }





        [HttpPost("Login")]
        public async Task<IResult> Login([FromBody] RegisterUserRequest request)
        {
            var context = HttpContext;

            var (accessToken, refreshToken, name, email, id) = await _usersService.Login( request.Email, request.Password);

            var registerUserResponse = new RegisterUserResponse(id, name, email);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Results.Ok(new { accessToken, refreshToken, user = registerUserResponse });
        }

        [HttpPost("Login")]
        public async Task<IResult> Refresh([FromBody] RegisterUserRequest request)
        {
            var context = HttpContext;

            var refreshTokenFromCookies = context.Request.Cookies["secretCookie"];

            if (string.IsNullOrEmpty(refreshTokenFromCookies))
            {
                throw new Exception();
            }
            var (accessToken, refreshToken, name, email, id) = await _usersService.Refresh(refreshTokenFromCookies);

            var registerUserResponse = new RegisterUserResponse(id, name, email);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Results.Ok(new { accessToken, refreshToken, user = registerUserResponse });
        }

    }
}

