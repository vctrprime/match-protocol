using System.Threading.Tasks;
using MatchProtocol.DTO.GameSettings;
using MatchProtocol.Games.Services.Abstract;
using MatchProtocol.Platform.Requests.Abstract;

namespace MatchProtocol.Games.Services.Concrete
{
    public class GameSettingsApiService : IGameSettingsApiService
    {
        private readonly IHttpRequestSender _httpRequestSender;

        public GameSettingsApiService(IHttpRequestSender httpRequestSender)
        {
            _httpRequestSender = httpRequestSender;
        }
        
        public async Task<WeatherGetDto> GetWeather()
        {
            var weather = await _httpRequestSender.GetAsync<WeatherGetDto>(
                "https://localhost:9012/api/weather",
                "get-weather.game-settings.service");

            return weather;
        }
    }
}