using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Region From Database - Domain Model
            var regionsDomain = dbContext.Regions.ToList();

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
        [Route("{id}")]
        public IActionResult GetRegion(Guid id)
        {
            // Use Find method only for primary key
            // Get Region From Database - Domain Model
            var regionDomain = dbContext.Regions.Find(id);

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
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto region)
        {
            // Map DTO to Domain Model
            var regionDomain = new Region
            {
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            // Add Region to Database
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO
            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }
    }
}
