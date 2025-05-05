using Microsoft.AspNetCore.Mvc;

namespace Image_Compression.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("compress")]
        public async Task<IActionResult> CompressAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var fileId = Guid.NewGuid().ToString("N");

            await SaveLargeImageAsync(file, fileId);
            await SaveSmallImageAsync(file, fileId);

            return Ok();
        }

        private async Task SaveLargeImageAsync(IFormFile file, string id) 
            => await SaveImageAsync(file, id, ImageType.Large);

        private async Task SaveSmallImageAsync(IFormFile file, string id) 
            => await SaveImageAsync(file, id, ImageType.Small);

        private async Task SaveImageAsync(IFormFile file, string id, ImageType imageType)
        {
            var fileName = $"{id}.{imageType.ToString().ToLower()}{Path.GetExtension(file.FileName)}";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageType.ToString().ToLower());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
        }
    }

    public enum ImageType
    {
        Large,
        Small
    };
}
