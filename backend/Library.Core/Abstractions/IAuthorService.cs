using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IAuthorsService
	{
        Task<List<Author>> GetAllAuthors();

    }
}

