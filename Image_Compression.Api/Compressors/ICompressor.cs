namespace Image_Compression.Api.Compressors
{
    public interface ICompressor
    {
        Task CompressAsync(IFormFile file, string fileId);
    }
}
