using BBRRevival.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BBRRevival.Server.Controllers
{
    [ApiController]
    [Route("/v1/path/db/")]
    public class PlanetController : ControllerBase
    {
        private readonly ILogger<PlanetController> _logger;
        private readonly IFilePacker filePacker;
        private readonly IPlanetService planetService;

        public PlanetController(ILogger<PlanetController> logger, IFilePacker filePacker, IPlanetService planetService)
        {
            _logger = logger;
            this.filePacker = filePacker;
            this.planetService = planetService;
        }

        [HttpGet]
        [Route("find")]
        public async Task<FileContentResult> FindPlanet()
        {
            string name = Request.Query["planet"];

            byte[] bytes = await planetService.GetPlanetBytes(name);

            byte[] response = filePacker.ZipBytes(bytes);

            return new FileContentResult(response, "application/octet-stream");
        }
    }
}
