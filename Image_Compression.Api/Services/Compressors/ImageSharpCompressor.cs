using Image_Compression.Api.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Image_Compression.Api.Services.Compressors
{
    public class ImageSharpCompressor : Compressor, ICompressor
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageSharpCompressor(IWebHostEnvironment webHostEnvironment)
            : base(webHostEnvironment)
            => _webHostEnvironment = webHostEnvironment;

        public async Task CompressAsync(string fileName, string fileId)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Original", fileName);
            using var image = await Image.LoadAsync<Rgba32>(path);
            image.Mutate(x => x.AutoOrient());

            await SaveResizedImageAsync(image, fileId, ImageType.Large, 1440);
            await SaveResizedImageAsync(image, fileId, ImageType.Medium, 450);
            await SaveResizedImageAsync(image, fileId, ImageType.Small, 267);
        }

        public async Task CompressAsync(IFormFile file, string fileId)
        {
            await SaveImageAsync(file, fileId, ImageType.Original);

            using var image = await Image.LoadAsync<Rgba32>(file.OpenReadStream());
            image.Mutate(x => x.AutoOrient());

            await SaveResizedImageAsync(image, fileId, ImageType.Large, 1440);
            await SaveResizedImageAsync(image, fileId, ImageType.Medium, 450);
            await SaveResizedImageAsync(image, fileId, ImageType.Small, 267);
        }

        private async Task SaveResizedImageAsync(Image<Rgba32> original, string fileId, ImageType imageType, int targetHeight)
        {
            string sizeLabel = imageType.ToString();

            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", sizeLabel);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, $"{fileId}.webp");

            var webpEncoder = new WebpEncoder
            {
                Quality = 75
            };

            // If original height is already small enough, just save it
            if (original.Height <= targetHeight)
            {
                await original.SaveAsWebpAsync(path, webpEncoder);
                return;
            }

            // Resize while maintaining aspect ratio
            original.Mutate(ctx => ctx.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(0, targetHeight) // Width=0 means auto-calculate
            }));

            await original.SaveAsWebpAsync(path, webpEncoder);
        }
    }
}
