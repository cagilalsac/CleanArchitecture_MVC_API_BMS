using Microsoft.AspNetCore.Mvc;
using Persistence.Seeds;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbController : ControllerBase
    {
        [HttpGet("Seed")]
        public IActionResult Seed()
        {
            new SeedDb().Initialize();
            return Ok("Database seed successful.");
        }
    }
}
