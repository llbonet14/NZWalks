using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;

        public WalksController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomain = mapper.Map<Walk>(addWalkRequestDto);



            return Ok();
        }
    }
}
