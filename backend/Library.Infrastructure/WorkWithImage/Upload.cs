using System;
using Library.Application.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.WorkWithImage
{
    public class Upload : IUpload
    {
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        public async Task<string> UploadImage(byte[] imageBytes, string fileName)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                throw new Exception("No image data provided.");
            }

            var filePath = Path.Combine(_imageFolderPath, fileName);

            // Создаем папку, если она не существует
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }

            // Записываем байты изображения в файл
            await File.WriteAllBytesAsync(filePath, imageBytes);

            return fileName;
        }
    }

}

