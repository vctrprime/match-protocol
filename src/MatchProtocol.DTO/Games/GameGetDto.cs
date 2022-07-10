using MatchProtocol.DTO.GameSettings;

namespace MatchProtocol.DTO.Games
{
    public class GameGetDto
    {
        public string Name { get; set; }
        public WeatherGetDto Weather { get; set; }
    }
}