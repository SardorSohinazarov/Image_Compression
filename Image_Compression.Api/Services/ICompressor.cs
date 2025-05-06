namespace Image_Compression.Api.Services
{
    public interface ICompressor
    {
        Task CompressAsync(IFormFile file, string fileId);
    }
}
