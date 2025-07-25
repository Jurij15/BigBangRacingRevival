using BBRRevival.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BBRRevival.Common.Responses.Player;

namespace BBRRevival.Server.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PreloadController> _logger;
        private readonly IPlayerService playerService;
        private readonly IClientConfigService clientConfigService;

        public PlayerController(ILogger<PreloadController> logger, IPlayerService playerService, IClientConfigService clientConfigService) 
        {
            _logger = logger;
            this.playerService = playerService;
            this.clientConfigService = clientConfigService;
        }

        [HttpPost]
        [Route("/v4/player/login")]
        public async Task<IActionResult> PlayerLogin()
        {
            _logger.LogInformation("Returning login");

            PlayerLoginResponse response = new();

            bool hasExisingUserId = false;
            if (Request.Headers["lastPathSync"] != 0)
            {
                //hasExisingUserId = true;
                _logger.LogWarning("NEW USER CREATION");
            }

            if (hasExisingUserId)
            {
                //logic for logging in
            }
            else
            {
                //logic for creating a new user
                response.AddPlayerData(await playerService.CreatePlayer());
                response.AddClientConfig(await clientConfigService.CreateClientConfig());
            }

            //add things like tournaments, events and planets in any way

            response.AddPlanetVersion(new("AdventureMotorcycle", 2));
            response.AddPlanetVersion(new("RacingOffroadCar", 2));
            response.AddPlanetVersion(new("AdventureOffroadCar", 2));
            response.AddPlanetVersion(new("RacingMotorcycle", 2));
            response.AddPlanetVersion(new("Metadata", 2));

            response.AddClientVersion(372);
            response.AddVersionInfo("VersionInfo");

            return Ok(response);
        }

        [HttpPost]
        [Route("/v2/player/data/change")]
        public async Task<IActionResult> UpdatePlayerData()
        {
            //TODO: make this actually work
            return Ok(new { lastPathSync = "now" });
        }

        [HttpGet]
        [Route("/v2/player/friends")]
        public async Task<IActionResult> GetPlayerFriends()
        {
            //todo: make this actually work
            return Ok(new { followees = new List<object>(), friends = new List<object>() });
        }
    }
}
