using Microsoft.AspNetCore.Mvc;
using Image_Compression.Api.Services;

namespace Image_Compression.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ICompressor _compressor;

        public ImagesController(ICompressor compressor) 
            => _compressor = compressor;

        [HttpPost("compress")]
        public async Task<IActionResult> CompressAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var fileId = Guid.NewGuid().ToString("N");

            await _compressor.CompressAsync(file, fileId);

            return Ok("Images saved in all sizes.");
        }
    }
}
