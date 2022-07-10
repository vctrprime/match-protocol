using System.Threading.Tasks;
using MatchProtocol.DTO.GameSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MatchProtocol.GameSettings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Authorize("getWeatherPolicy")]
        public WeatherGetDto Get()
        {
            _logger.LogInformation("test");
            var result = new WeatherGetDto
            {
                Temperature = 66
            };
            return result;
        }

    }
}