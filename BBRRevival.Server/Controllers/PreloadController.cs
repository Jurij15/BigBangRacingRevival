using Microsoft.AspNetCore.Mvc;

namespace BBRRevival.Server.Controllers
{
    [ApiController]
    [Route("/v1/preload/")]
    public class PreloadController : ControllerBase
    {
        private readonly ILogger<PreloadController> _logger;

        public PreloadController(ILogger<PreloadController> logger)
        {
            _logger = logger;
        }

        [HttpGet("checkVersion")]
        public async Task<IActionResult> checkVersion()
        {
            _logger.LogInformation("Returning checkVersion");
            return Ok(new { version = "upToDate"}); //TODO: add an actuall version check
        }
    }
}
