using System;
using Library.API.Contracts;
using Library.Application.Commands.Authors;
using Library.Application.Commands.Books;
using Library.Application.Commands.Users;
using Library.Core.Models;
using Mapster;

namespace Library.API.Mapping
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            // Маппинг AuthorRequest -> AddAuthorCommand
            TypeAdapterConfig<AuthorRequest, AddAuthorCommand>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.firstName)
                .Map(dest => dest.Surname, src => src.surname)
                .Map(dest => dest.Birthday, src => src.birthday)
                .Map(dest => dest.Country, src => src.country);

            // Маппинг UpdateAuthorRequest -> UpdateAuthorCommand
            TypeAdapterConfig<UpdateAuthorRequest, UpdateAuthorCommand>
                .NewConfig()
                .Map(dest => dest.Id, src => src.id)
                .Map(dest => dest.FirstName, src => src.firstName)
                .Map(dest => dest.Surname, src => src.surname)
                .Map(dest => dest.Birthday, src => src.birthday)
                .Map(dest => dest.Country, src => src.country);

            // Маппинг Author -> AuthorsResponse
            TypeAdapterConfig<Author, AuthorsResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Country, src => src.Country);

            // Маппинг BookRequest -> AddBookCommand
            TypeAdapterConfig<AddBookRequest, AddBookCommand>
                .NewConfig()
                .Map(dest => dest.Title, src => src.title)
                .Map(dest => dest.ISBN, src => src.isbn)
                .Map(dest => dest.Description, src => src.description)
                .Map(dest => dest.RecieveDate, src => src.recieveDate)
                .Map(dest => dest.ReturnDate, src => src.returnDate)
                .Map(dest => dest.Genre, src => src.genre)
                .Map(dest => dest.AuthorId, src => src.author)
                .Map(dest => dest.ImageName, src => src.imageName);

            // Маппинг BookRequest -> UpdateBookCommand
            TypeAdapterConfig<BookRequest, UpdateBookCommand>
                .NewConfig()
                .Map(dest => dest.Book.Id, src => src.id)
                .Map(dest => dest.Book.Title, src => src.title)
                .Map(dest => dest.Book.ISBN, src => src.isbn)
                .Map(dest => dest.Book.Description, src => src.description)
                .Map(dest => dest.Book.RecieveDate, src => src.recieveDate)
                .Map(dest => dest.Book.ReturnDate, src => src.returnDate)
                .Map(dest => dest.Book.Genre, src => src.genre)
                .Map(dest => dest.Book.AuthorId, src => src.author)
                .Map(dest => dest.Book.UserId, src => src.user)
                .Map(dest => dest.Book.ImageName, src => src.imageName);

            // Маппинг Book -> BookResponse
            TypeAdapterConfig<Book, BookResponse>
                .NewConfig()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.isbn, src => src.ISBN)
                .Map(dest => dest.description, src => src.Description)
                .Map(dest => dest.recieveDate, src => src.RecieveDate)
                .Map(dest => dest.returnDate, src => src.ReturnDate)
                .Map(dest => dest.genre, src => src.Genre)
                .Map(dest => dest.author, src => src.AuthorId)
                .Map(dest => dest.user, src => src.UserId)
                .Map(dest => dest.imageName, src => src.ImageName);

            // Маппинг Book -> BooksResponse
            TypeAdapterConfig<Book, BooksResponse>
                .NewConfig()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.isbn, src => src.ISBN)
                .Map(dest => dest.description, src => src.Description)
                .Map(dest => dest.recieveDate, src => src.RecieveDate)
                .Map(dest => dest.returnDate, src => src.ReturnDate)
                .Map(dest => dest.genre, src => src.Genre)
                .Map(dest => dest.author, src => src.AuthorId);


            TypeAdapterConfig<RegisterUserRequest, RegisterUserCommand>.NewConfig()
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Password, src => src.Password)
           .Map(dest => dest.Email, src => src.Email);

            // Маппинг для LoginUserRequest -> LoginUserCommand
            TypeAdapterConfig<LoginUserRequest, LoginUserCommand>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);

        }
    }


}

