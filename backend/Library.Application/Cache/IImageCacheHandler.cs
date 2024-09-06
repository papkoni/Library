using System;
using Library.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Cache
{
	public interface IImageCacheHandler
	{
        Task<byte[]?> GetImageAsync(string imageKey);
    }
}

