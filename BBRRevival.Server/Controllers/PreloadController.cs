using BBRRevival.Common.Responses.Preload;
using BBRRevival.Server.Interfaces;
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
        private readonly IMusicService musicService; 

        public PreloadController(ILogger<PreloadController> logger, IMusicService music)
        {
            _logger = logger;
            musicService = music;
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

            string name = Request.QueryString.ToString().Split("&")[1].Remove(0, 5); //todo: replace this with properties

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
        //[Produces("application/octet-stream")] //override json response
        public async Task<IActionResult> downloadFile()
        {
            _logger.LogInformation("Returning downloadFile");

            string name = Request.QueryString.ToString().Replace("?", ""); //todo: replace this with properties

            byte[] music = null;

            music = await musicService.GetMusicFile(name);

            if (music is null)
            {
                return NotFound();
            }

            return Ok(music);
        }
    }
}
