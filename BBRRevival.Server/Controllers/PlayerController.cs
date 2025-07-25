using BBRRevival.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BBRRevival.Server.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PreloadController> _logger;
        private readonly IPlayerService playerService;

        public PlayerController(ILogger<PreloadController> logger, IPlayerService playerService) 
        {
            _logger = logger;
            this.playerService = playerService;
        }

        [Route("/v4/player/login")]
        public IActionResult PlayerLogin()
        {
            _logger.LogInformation("Returning login");

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
            }

            return Ok();
        }
    }
}
