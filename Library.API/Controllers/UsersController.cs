using Library.API.Contracts;
using Library.Application.Services;
using Library.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUsersService _usersService;


        public UsersController(IHttpContextAccessor httpContextAccessor, IUsersService usersService)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersService = usersService;
        }


        [HttpPost("Register")]
        public  async Task<IResult> Register(
        [FromBody] RegisterUserRequest request
        
        )
        {
            await _usersService.Register(request.Name, request.Password, request.Email);

            return Results.Ok();
        }

        
       

        [HttpPost("Login")]
        public async Task<IResult> Login(LoginUserRequest request
            )
        {
            var context = HttpContext;

            var token = await _usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("secretCookie", token);

            return Results.Ok(token);
        }

    }
}

