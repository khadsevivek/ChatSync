using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigletoneDesignPatternImplementation.Services;

namespace SigletoneDesignPatternImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignPatternTesterController : ControllerBase
    {
        [HttpGet("First")]
        public async Task<ActionResult> TestFirstInstence()
        {
            var obj = Singletone.getInstence();
            return Ok(Singletone.TestId);
        }
        [HttpGet("Second")]
        public async Task<ActionResult> TestSecoundInstence()
        {
            var obj = Singletone.getInstence();
            return Ok(Singletone.TestId);
        }
    }
}
