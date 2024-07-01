using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {
            if (request is null)
                return BadRequest("File is null");

            ValidateFileUpload(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", request.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully");
        }

        public void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
                ModelState.AddModelError("file", "Unsupported file extension");

            if (request.File.Length > 10485760)
                ModelState.AddModelError("file", "File is larger than 10MB");
        }
    }
}
