using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

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
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRegion(Guid id)
        {
            //var region = dbContext.Regions.Where(r => r.Id == id).SingleOrDefault();
            //Use Find method only for primary key
            var region = dbContext.Regions.Find(id);

            return region == null ? NotFound() : Ok(region);
        }
    }
}
