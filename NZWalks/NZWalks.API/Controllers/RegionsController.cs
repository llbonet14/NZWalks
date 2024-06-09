using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
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
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Region From Database - Domain Model
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Model to DTO
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

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
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            // Return DTO
            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto region)
        {
            // Map DTO to Domain Model
            var regionDomain = mapper.Map<Region>(region);

            // Add Region to Database
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            // Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            // Return DTO
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDto region)
        {
            // Map UpdateDTO to Domain Model
            var regionUpdateModel = mapper.Map<Region>(region);

            // Get Region From Database - Domain Model
            regionUpdateModel = await regionRepository.UpdateAsync(id, regionUpdateModel);

            if (regionUpdateModel == null)
                return NotFound();

            // Map Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionUpdateModel);

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

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}
