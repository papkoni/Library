namespace Library.Application.Handlers
{
	public class ImageUnloadHandler
	{

        private readonly string _imageDirectory;

        public ImageUnloadHandler()
		{
            _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "Library.Infrastructure", "Images");

        }

        public (byte[]? imageBytes, string? mimeType) GetImage(string fileName)
        {
            var filePath = Path.Combine(_imageDirectory, fileName);

            if (!File.Exists(filePath))
            {
                return (null, null); //ДОБАВИТЬ НОРМ ОШИБКУ
            }

            var mimeType = GetMimeType(filePath);
            var imageBytes = File.ReadAllBytes(filePath);

            return (imageBytes, mimeType);
        }

        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

    }
}

