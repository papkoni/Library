using System;
using Library.Core.Models;

namespace Library.API.Contracts
{
	
    public record AuthorsResponse(
       Guid id,
       string firstName,
       string Surname,
       DateTime? Birthday,
       string? Country,
       List<BookWithoutImageResponse>? Books
       
       );



}

