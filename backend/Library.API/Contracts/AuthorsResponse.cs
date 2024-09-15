using System;
using Library.Core.Models;

namespace Library.API.Contracts
{
	
    public record AuthorsResponse(
       Guid Id,
       string FirstName,
       string Surname,
       DateTime? Birthday,
       string? Country
       
       
       );

    //List<BookWithoutImageResponse>? Books

}

