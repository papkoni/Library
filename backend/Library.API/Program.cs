

using System.Text;
using Library.Application.Auth;
using Library.Application.Services;
using Library.Core.Abstractions;
using Library.DataAccess;
using Library.DataAccess.Mapper;
using Library.DataAccess.Repositories;
using Library.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static CSharpFunctionalExtensions.Result;
using Library.API.Extensions;
using Library.API.Middlewares;

namespace Library.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor(); 


        builder.Services.AddControllers();

        builder.Services.AddApiAuthentication(builder.Configuration);
        


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));


        builder.Services.AddDbContext<LibraryDbContext>(
           options =>
           {
               options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LibraryDbContext)));
           }
           );

        builder.Services.AddStackExchangeRedisCache(options =>
            options.Configuration = builder.Configuration.GetConnectionString("Cache"));

        builder.Services.AddScoped<IBooksRepository, BooksRepository>();
        builder.Services.AddScoped<IBooksService, BooksService>();
        builder.Services.AddScoped<IUsersService, UsersService>();

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();


        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();




        builder.Services.AddAutoMapper(typeof(MapperProfile));

       


       

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }
        app.UseHttpsRedirection();

        //app.UseCustomExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

