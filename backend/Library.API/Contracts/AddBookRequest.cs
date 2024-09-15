using System;
namespace Library.API.Contracts
{
    public record AddBookRequest(

       string title,
       string isbn,
       string description,
       DateTime? recieveDate,
       DateTime? returnDate,
       string genre,
       Guid author,
       byte[]? imageByte,
       string imageName
       );
}

