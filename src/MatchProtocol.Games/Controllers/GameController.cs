using System;
using System.Security;
using System.Threading.Tasks;
using MatchProtocol.DTO.Games;
using MatchProtocol.Games.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchProtocol.Games.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class GameController : ControllerBase
    {
        private readonly IGameSettingsApiService _gameSettingsApiService;

        public GameController(IGameSettingsApiService gameSettingsApiService)
        {
            _gameSettingsApiService = gameSettingsApiService;
        }

        [HttpPost]
        [Authorize("webClientPolicy")]
        public async Task<IActionResult> Post(GamePostDto game)
        {
            try
            {
                var weather = await _gameSettingsApiService.GetWeather();
                var result = new GameGetDto
                {
                    Name = "test",
                    Weather = weather
                };

                return Ok(result);
            }
            catch(SecurityException se)
            {
                return BadRequest(se.Message);
            }
           
        }

    }
}