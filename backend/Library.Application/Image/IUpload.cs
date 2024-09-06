using System;
using Microsoft.AspNetCore.Http;

namespace Library.Application.Image
{
	public interface IUpload
	{
        Task<string> UploadImage(IFormFile file);

    }
}

