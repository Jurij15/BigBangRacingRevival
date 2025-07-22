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
        public async Task<IActionResult> CheckVersion()
        {
            _logger.LogInformation("Returning checkVersion");

            CheckVersionResponse response = new()
            {
                version = "upToDate"
            };

            return Ok(response); //TODO: add an actuall version check
        }

        [HttpGet("checkFile")]
        public async Task<IActionResult> CheckFile()
        {
            _logger.LogInformation("Returning checkFile");

            var request = HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host}";

            string name = string.Empty;

            try
            {
                name = Request.QueryString.ToString().Split("&")[1].Remove(0, 5); //todo: replace this with properties 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading file name");
                return NotFound("Filename is missing!");
            }

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
        public async Task<IActionResult> DownloadFile()
        {
            _logger.LogInformation("Returning downloadFile");

            string name = string.Empty;

            try
            {
                name = Request.QueryString.ToString().Replace("?", ""); //todo: replace this with properties
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading file name");
                return NotFound("Filename is missing!");
            }

            byte[] music = null;

            music = await musicService.GetMusicFile(name);

            if (music is null)
            {
                return NotFound($"File with name {name}.bank not found");
            }

            return Ok(music);
        }
    }
}
