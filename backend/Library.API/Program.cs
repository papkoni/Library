

using Library.Application.Auth;
using Library.Core.Abstractions;
using Library.DataAccess;

using Library.DataAccess.Repositories;
using Library.Infrastructure.Authentication;

using Microsoft.EntityFrameworkCore;

using Library.API.Extensions;
using Library.API.Middlewares;
using Library.Infrastructure.WorkWithImage;
using Library.Application.Cache;
using Library.Application.Image;
using Mapster;
using MapsterMapper;
using Library.Application.Handlers.Books;
using Library.API.Mapping;

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
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        //builder.Services.AddScoped<IAuthorsService, AuthorsService>();

        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();

        builder.Services.AddScoped<IImageCacheHandler, ImageCacheHandler>();
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

        builder.Services.AddScoped<IUpload, Upload>();
        //builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBooksQueryHandler).Assembly));
        // Регистрация Mapster с DI
        builder.Services.AddSingleton<TypeAdapterConfig>();

        // Регистрируем автоматически все маппинги, имплементирующие IRegister
        builder.Services.AddSingleton<IMapper, ServiceMapper>();

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontendApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // Включаем поддержку отправки cookies
            });
        }
        );

        MappingConfig.RegisterMappings();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }
        app.UseCors("AllowFrontendApp"); // Применяем политику CORS перед UseRouting
        app.UseHttpsRedirection();

        app.UseCustomExceptionHandler();
        app.UseAuthentication();
        app.UseAuthorization();

       
        app.MapControllers();

        app.Run();
    }
}

