using System;
using Library.Application.Commands.Authors;
using Library.Core.Models;
using Mapster;

namespace Library.Infrastructure.Mapper
{
    public class AuthorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Маппинг из Author в AddAuthorCommand
            config.NewConfig<Author, AddAuthorCommand>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Country, src => src.Country);


            // Маппинг из AddAuthorCommand в Author
            config.NewConfig<AddAuthorCommand, Author>()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.Surname, src => src.Surname)
                .Map(dest => dest.Birthday, src => src.Birthday)
                .Map(dest => dest.Country, src => src.Country)

                .ConstructUsing(src => Author.Create(Guid.NewGuid(), src.FirstName, src.Surname, src.Birthday, src.Country));
        }
    }
}

