using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        public HelloController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
