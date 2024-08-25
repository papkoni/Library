using System;
namespace Library.API.Contracts
{
    //приходит с фронта
    public record BooksRequest(
        
        string title,
        string isbn,
        string description,
        string gender,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        string authorName);
}

