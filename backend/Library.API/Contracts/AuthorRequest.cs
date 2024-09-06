using System;
namespace Library.API.Contracts
{
    public record AuthorRequest
    (
         string firstName,
         string surname, 
         DateTime? birthday ,
         string? country,
         List<BookWithoutImageResponse>? books 
    );
}

