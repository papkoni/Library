using System;
namespace Library.API.Contracts
{
    public record BookWithoutImageResponse(
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

