using Microsoft.AspNetCore.Mvc;

namespace ExtrossServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from MyController!");
        }
    }
}
