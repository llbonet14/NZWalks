using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Region From Database - Domain Model
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Model to DTO
            var regionsDto = regionsDomain.Select(r => new RegionDto
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                RegionImageUrl = r.RegionImageUrl
            }).ToList();

            // Return DTO
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Use Find method only for primary key
            // Get Region From Database - Domain Model
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
                return NotFound();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
        {
            // Map DTO to Domain Model
            var regionDomain = new Region
            {
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            // Add Region to Database
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDto region)
        {
            var regionUpdateModel = new Region
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Get Region From Database - Domain Model
            regionUpdateModel = await regionRepository.UpdateAsync(id, regionUpdateModel);

            if (regionUpdateModel == null)
                return NotFound();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionUpdateModel.Id,
                Name = regionUpdateModel.Name,
                Code = regionUpdateModel.Code,
                RegionImageUrl = regionUpdateModel.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Get Region From Database - Domain Model
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null)
                return NotFound();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
