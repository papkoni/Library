using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Library.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Передача запроса следующему middleware
                await _next(context);
            }
            catch (Exception exception)
            {
                // Обработка исключений, если они возникают
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            string result;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonSerializer.Serialize(new { error = "Unauthorized access" });
                    break;
                case ArgumentException argumentException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = argumentException.Message });
                    break;
                case KeyNotFoundException keyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = keyNotFoundException.Message });
                    break;
                case DbUpdateException dbUpdateException:
                    code = HttpStatusCode.Conflict;
                    result = JsonSerializer.Serialize(new { error = "Database update error" });
                    break;
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = validationException.Message });
                    break;
                default:
                    result = JsonSerializer.Serialize(new { error = "An unexpected error occurred. " + $"{exception}" });
                    break;
            }


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(result);
        }

    }

}

