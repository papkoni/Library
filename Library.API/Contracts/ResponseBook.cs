using System;
namespace Library.API.Contracts
{
	
    //идет на фронт
    public record ResponseBook(
        Guid id,
        string title,
        string isbn,
        string description,
        string gender,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        Guid authorID);
}

