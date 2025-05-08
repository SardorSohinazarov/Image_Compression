using Image_Compression.Api.Models;
using ImageMagick;

namespace Image_Compression.Api.Services.Compressors
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

        public async Task CompressAsync(string fileName, string fileId)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
            using var image = new MagickImage(path);
            image.Format = MagickFormat.WebP;
            image.Quality = 75;

            await SaveResizedImageAsync(image, fileId, ImageType.Large, 1440);
            await SaveResizedImageAsync(image, fileId, ImageType.Medium, 450);
            await SaveResizedImageAsync(image, fileId, ImageType.Small, 267);
        }

        private async Task SaveResizedImageAsync(MagickImage original, string fileId, ImageType imageType, int targetHeight)
        {
            string sizeLabel = imageType.ToString();

            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", sizeLabel);
            var path = Path.Combine(folder, $"{fileId}.webp");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            
            if (original.Height <= targetHeight)
            {
                await original.WriteAsync(new FileInfo(path));
                return;
            }

            var clone = original.Clone();
            clone.Resize(0, (uint)targetHeight); // Maintain aspect ratio by setting width = 0

            await clone.WriteAsync(new FileInfo(path));
        }
    }
}
