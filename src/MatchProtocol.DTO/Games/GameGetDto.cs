using MatchProtocol.DTO.GameSettings;

namespace MatchProtocol.DTO.Games
{
    public class GameGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WeatherGetDto Weather { get; set; }
    }
}