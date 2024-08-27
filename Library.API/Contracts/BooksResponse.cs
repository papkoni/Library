﻿using System;
namespace Library.API.Contracts
{
	
    //идет на фронт
    public record BooksResponse(
        Guid id,
        string title,
        string isbn,
        string description,
        DateTime? recieveDate,
        DateTime? returnDate,
        string genre,
        Guid authorID);
}

