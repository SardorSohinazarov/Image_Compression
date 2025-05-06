using Image_Compression.Api.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace Image_Compression.Api.Services.Compressors
{
    public class BitmapCompressor : Compressor, ICompressor
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BitmapCompressor(IWebHostEnvironment webHostEnvironment)
            : base(webHostEnvironment) 
            => _webHostEnvironment = webHostEnvironment;

        public async Task CompressAsync(IFormFile file, string fileId)
        {
            await SaveImageAsync(file, fileId, ImageType.Original);

            using var stream = file.OpenReadStream();
            using var original = new Bitmap(stream);

            await SaveResizedImageAsync(original, fileId, ImageType.Large, 1440);
            await SaveResizedImageAsync(original, fileId, ImageType.Medium, 450);
            await SaveResizedImageAsync(original, fileId, ImageType.Small, 267);
        }

        private async Task SaveResizedImageAsync(Bitmap original, string fileId, ImageType imageType, int targetHeight)
        {
            string sizeLabel = imageType.ToString();
            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", sizeLabel);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var path = Path.Combine(folder, $"{fileId}.webp");

            int width = (int)((double)targetHeight / original.Height * original.Width);

            if (original.Height <= targetHeight)
            {
                original.Save(path, ImageFormat.Webp);
                return;
            }

            using var resized = new Bitmap(original, new Size(width, targetHeight));
            resized.Save(path, ImageFormat.Webp);

            await Task.CompletedTask;
        }
    }
}
