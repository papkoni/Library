using System;
using AutoMapper;
using Library.Core.Models;
using Library.DataAccess.Entites;

namespace Library.DataAccess.Mapper.Extensions
{
    public static class MapToModelExtension
    {
        public static Book MapToModel(this BookEntity? bookEntity, IMapper mapper) => mapper.Map<Book>(bookEntity);

    }
}

