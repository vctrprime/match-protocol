using System.Threading.Tasks;
using MatchProtocol.DTO.GameSettings;

namespace MatchProtocol.Games.Services.Abstract
{
    public interface IGameSettingsApiService
    {
        Task<WeatherGetDto> GetWeather();
    }
}