using AutoMapper;
using Library.Core.Models;
using Library.DataAccess.Entites;

namespace Library.DataAccess.Mapper
{
	public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            //CreateMap<BookEntity, Book>()
            // .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))    // Маппинг объекта AuthorEntity к Author
            // .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));        // Маппинг объекта UserEntity к User

            //// Маппинг из Book в BookEntity
            //CreateMap<Book, BookEntity>()
            // .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author)) // Map the entire Author object
            // .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))     // Map the entire User object
            // .ForPath(dest => dest.Author.Id, opt => opt.MapFrom(src => src.Author.Id)) // Map the AuthorId property from the Author object
            // .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id)); // Map the UserId property from the User object

            //CreateMap<AuthorEntity, Author>()
            //           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //           .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            //           .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            //           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            //           .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            //CreateMap<Author, AuthorEntity>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            //    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            //    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            //    .ForMember(dest => dest.Books, opt => opt.Ignore()); // Игнорируем книги для маппинга в обратную сторону

            //CreateMap<UserEntity, User>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            //CreateMap<User, UserEntity>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}

