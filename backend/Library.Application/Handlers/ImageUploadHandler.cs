using Microsoft.AspNetCore.Http;

namespace Library.Application.Handlers
{
	public class ImageUploadHandler
	{

        private readonly string _imageDirectory;

        public ImageUploadHandler()
        {
            _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "Library.Infrastructure", "Images");
        }

        public string Upload(IFormFile file)
        {
            // Extension
            List<string> validExtentions = new List<string>() { ".jpg", ".png", ".jpeg" };
            string extention = Path.GetExtension(file.FileName);
            if (!validExtentions.Contains(extention))
            {
                return $"Extention is not valid ({string.Join(',', validExtentions)})";
            }

            // File size
            long size = file.Length;
            if (size > (5 * 1024 * 1024))
            {
                return "Maximum size can be 5mb";
            }

            // Name changing
            string fileName = Guid.NewGuid().ToString() + extention;

            using FileStream stream = new FileStream(Path.Combine(_imageDirectory, fileName), FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }

    }
}

