using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ExtrossServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from ProtectedController!");
        }
    }
}
