
namespace Image_Compression.Api.Compressors
{
    public class BitmapCompressor : Compressor, ICompressor
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BitmapCompressor(IWebHostEnvironment webHostEnvironment)
            : base(webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CompressAsync(IFormFile file, string fileId)
        {
            throw new NotImplementedException("Bitmap compression is not implemented yet.");
        }
    }
}
