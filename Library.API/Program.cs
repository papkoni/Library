

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
namespace Library.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor(); 

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddApiAuthentication(builder.Configuration);
        //builder.Services.AddAuthentication(opt =>
        //{
        //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = "https://localhost:5001",
        //        ValidAudience = "https://localhost:5001",
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        //    };
        //});


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));


        builder.Services.AddDbContext<LibraryDbContext>(
           options =>
           {
               options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LibraryDbContext)));
           }
           );

        builder.Services.AddScoped<IBooksRepository, BooksRepository>();
        builder.Services.AddScoped<IBooksService, BooksService>();
        builder.Services.AddScoped<IUsersService, UsersService>();

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();


        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();




        builder.Services.AddAutoMapper(typeof(MapperProfile));

       


       

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

