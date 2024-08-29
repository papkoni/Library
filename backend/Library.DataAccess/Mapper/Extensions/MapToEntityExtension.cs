using System;
using AutoMapper;
using Library.Core.Models;
using Library.DataAccess.Entites;

namespace Library.DataAccess.Mapper.Extensions
{
	public static class MapToEntityExtension
	{

        public static BookEntity MapToEntity(this Book? book, IMapper mapper) => mapper.Map<BookEntity>(book);

    }
}

