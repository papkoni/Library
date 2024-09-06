

using Library.Application.Cache;
using Library.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Library.Infrastructure.WorkWithImage
{
    public class ImageCacheHandler : IImageCacheHandler
    {
        private readonly IDistributedCache _cache;
        private readonly string _imageFolderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Images"));

       

        public ImageCacheHandler(IDistributedCache cache)
        {
            _cache = cache;
            
        }

        public async Task<byte[]?> GetImageAsync(string imageKey)
        {
            var imageBytes = await _cache.GetAsync(imageKey);
            if (imageBytes != null)
            {
                Console.WriteLine("I from cache");
                return imageBytes;
            }

            imageBytes = await LoadImageFromSourceAsync(imageKey);

            if (imageBytes == null)
            {
                return null;
            }

            await _cache.SetAsync(imageKey, imageBytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });

            return imageBytes;
        }

        private async Task<byte[]?> LoadImageFromSourceAsync(string imageFileName)
        {
            var filePath = Path.Combine(_imageFolderPath, imageFileName);

            if (!File.Exists(filePath))
            {
                throw new Exception("ImageName == null");
            }

            byte[] imageBytes;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }

            return imageBytes;
        }

       
    }
}