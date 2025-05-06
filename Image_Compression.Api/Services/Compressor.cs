using Image_Compression.Api.Models;

namespace Image_Compression.Api.Services
{
    public abstract class Compressor
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Compressor(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected async Task SaveImageAsync(IFormFile file, string fileId, ImageType imageType)
        {
            var fileName = $"{fileId}{Path.GetExtension(file.FileName)}";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageType.ToString());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
