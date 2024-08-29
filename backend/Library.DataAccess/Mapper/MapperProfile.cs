using AutoMapper;
using Library.Core.Models;
using Library.DataAccess.Entites;

namespace Library.DataAccess.Mapper
{
	public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookEntity, Book>();
            CreateMap<Book, BookEntity>();

        }
    }
}

