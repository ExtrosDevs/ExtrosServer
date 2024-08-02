using Microsoft.AspNetCore.Mvc;

namespace ExtrossServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from MyController!");
        }
    }
}
