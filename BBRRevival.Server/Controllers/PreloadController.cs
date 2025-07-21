using BBRRevival.Common.Responses.Preload;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

            CheckVersionResponse response = new()
            {
                version = "upToDate"
            };

            return Ok(response); //TODO: add an actuall version check
        }

        [HttpGet("checkFile")]
        public async Task<IActionResult> checkFile()
        {
            _logger.LogInformation("Returning checkFile");

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}";

            string name = Request.QueryString.ToString().Split("&")[1].Remove(0, 5);

            CheckFileResponse response = new()
            {
                name = name,
                type = "music",
                path = baseUrl += $"/v1/preload/downloadFile?{name}",
                version = 0
            };

            return Ok(response);

        }

        [HttpGet("downloadFile")]
        public async Task<IActionResult> downloadFile()
        {
            _logger.LogInformation("Returning downloadFile");
            return Ok();
        }
    }
}
