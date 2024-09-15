using System;
namespace Library.API.Contracts
{
	public record UpdateAuthorRequest
    (
        Guid id,
         string firstName,
         string surname,
         DateTime? birthday,
         string? country
    //List<BookWithoutImageResponse>? books 
    );
}

