namespace Library.API.Middlewares
{
    using Library.Application.Auth;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtProvider _jwtProvider;

        public JwtMiddleware(RequestDelegate next, IJwtProvider jwtProvider)
        {
            _next = next;
            _jwtProvider = jwtProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var accessToken = authorizationHeader.Split(' ')[1];
                if (string.IsNullOrEmpty(accessToken))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userData = _jwtProvider.ValidateAccessToken(accessToken);
                if (userData == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                context.Items["User"] = userData;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Передаем управление следующему middleware в конвейере
            await _next(context);
        }
    }

}

