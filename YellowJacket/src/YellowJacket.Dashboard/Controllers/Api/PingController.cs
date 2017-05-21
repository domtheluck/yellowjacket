using Microsoft.AspNetCore.Mvc;

namespace YellowJacket.Dashboard.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/v1/ping")]
    public class PingController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ping Back");
        }
    }
}