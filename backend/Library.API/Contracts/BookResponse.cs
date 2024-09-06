using System;
using Library.Core.Models;

namespace Library.API.Contracts
{
    public record BookResponse(
        Guid id,
        string title,
        string isbn,
        string description,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        Guid author,
        Guid? user,
        byte[] imageByte,
        string imageName
        );

    public record BookWithotImageResponse(
        Guid id,
        string title,
        string isbn,
        string description,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        Guid author,
        Guid? user,
        
        string imageName
        );
}

