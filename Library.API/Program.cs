
using Library.Application.Services;
using Library.Core.Abstractions;
using Library.DataAccess;
using Library.DataAccess.Mapper;
using Library.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<LibraryDbContext>(
           options =>
           {
               options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(LibraryDbContext)));
           }
           );

        builder.Services.AddScoped<IBooksRepository, BooksRepository>();
        builder.Services.AddScoped<IBooksService, BooksService>();


        builder.Services.AddAutoMapper(typeof(MapperProfile));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

