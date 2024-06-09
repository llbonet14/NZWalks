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
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomain = mapper.Map<Walk>(addWalkRequestDto);
            var createdWalk = await walkRepository.CreateAsync(walkDomain);
            var walkDto = mapper.Map<WalkDto>(createdWalk);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await walkRepository.GetAllAsync();
            var walkDtos = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id:guid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
                return NotFound();

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }
    }
}
