using System;
using Library.Application.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.WorkWithImage
{
	public class Upload: IUpload
    {

        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("no file");
            }


           
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_imageFolderPath, fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}

