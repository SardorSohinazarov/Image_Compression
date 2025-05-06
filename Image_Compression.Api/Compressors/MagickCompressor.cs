using Image_Compression.Api.Models;
using ImageMagick;

namespace Image_Compression.Api.Compressors
{
    public class MagickCompressor : Compressor, ICompressor
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MagickCompressor(IWebHostEnvironment webHostEnvironment) 
            : base(webHostEnvironment) 
            => _webHostEnvironment = webHostEnvironment;

        public async Task CompressAsync(IFormFile file, string fileId)
        {
            await SaveImageAsync(file, fileId, ImageType.Original);

            using var image = new MagickImage(file.OpenReadStream());
            image.Format = MagickFormat.WebP;
            image.Quality = 75;

            await SaveResizedImageAsync(image, fileId, ImageType.Large, 1440);
            await SaveResizedImageAsync(image, fileId, ImageType.Medium, 450);
            await SaveResizedImageAsync(image, fileId, ImageType.Small, 267);
        }

        private async Task SaveResizedImageAsync(MagickImage original, string fileId, ImageType imageType, int targetHeight)
        {
            string sizeLabel = imageType.ToString();

            if (original.Height <= targetHeight)
            {
                var folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", sizeLabel);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                var path = Path.Combine(folder, $"{fileId}.webp");
                await original.WriteAsync(new FileInfo(path));
                return;
            }

            var clone = original.Clone();
            clone.Resize(0, (uint)targetHeight); // Maintain aspect ratio by setting width = 0

            var targetFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", sizeLabel);
            if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

            var filePath = Path.Combine(targetFolder, $"{fileId}.webp");
            await clone.WriteAsync(new FileInfo(filePath));
        }
    }
}
