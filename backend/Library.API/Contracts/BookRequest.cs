using System;
using Library.Core.Models;

namespace Library.API.Contracts
{
    //приходит с фронта
    public record BookRequest(
        
        string title,
        string isbn,
        string description,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        User user,
        Author author,
        IFormFile imageFile
        );

}

