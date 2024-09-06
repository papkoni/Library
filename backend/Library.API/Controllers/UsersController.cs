using System.Security.Claims;
using Library.API.Contracts;
using Library.Application.Services;
using Library.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var context = HttpContext;

            var (accessToken, refreshToken) = await _usersService.Register(request.Name, request.Password, request.Email);


            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken });
        }





        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var context = HttpContext;

            var (accessToken, refreshToken , user) = await _usersService.Login( request.Email, request.Password);


            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken, user });
        }
        
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh()
        {
            Console.WriteLine("suka");
            var context = HttpContext;

            var refreshTokenFromCookies = context.Request.Cookies["secretCookie"];

            if (string.IsNullOrEmpty(refreshTokenFromCookies))
            {
                return Unauthorized(new { message = "Refresh token is missing or invalid." });
            }
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var (accessToken, refreshToken, user) = await _usersService.Refresh(refreshTokenFromCookies, userIdClaim);

            context.Response.Cookies.Append("secretCookie", refreshToken);

            return Ok(new { accessToken, refreshToken, user });
        }

        //валедейшен аксес
        [Authorize]
        [HttpGet("CheckAccess")]
        public async  Task<IActionResult> CheckAccess()
        {


            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                

                var user = await _usersService.CheckAccess(userIdClaim);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "yyyyy");
            }
        }


        [HttpGet("Logout")]
        //[Authorize(Policy = "UserOrAdmin")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("secretCookie");

            return Ok();
        }

    }
}

