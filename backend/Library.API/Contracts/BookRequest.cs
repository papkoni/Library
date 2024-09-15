using System;
using Library.Core.Models;

namespace Library.API.Contracts
{
    //приходит с фронта
    public record BookRequest(
        Guid id,
        string title,
        string isbn,
        string description,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        Guid? user,
        Guid author,
        byte[] imageFile,
        string imageName
        );

}

