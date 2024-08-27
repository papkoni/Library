using System;
using Library.Core.Models;

namespace Library.Core.Abstractions
{
	public interface IRefreshTokensRepository
	{
        Task Add(RefreshToken refreshToken);

    }
}

