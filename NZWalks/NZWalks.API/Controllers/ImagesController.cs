using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper, IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {
            if (request is null)
                return BadRequest("File is null");

            ValidateFileUpload(request);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageDomain = mapper.Map<Image>(request);

            await imageRepository.Upload(imageDomain);

            return Ok(imageDomain);
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
