using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IAuthorRepository
	{
        Task<List<Author>> GetAllAuthors();

    }
}

