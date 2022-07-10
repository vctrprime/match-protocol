using System.Threading.Tasks;
using MatchProtocol.DTO.Games;

namespace MatchProtocol.WebUI.Services.Abstract
{
    public interface IApiGatewayService
    {
        Task<GameGetDto> CreateGame(GamePostDto game);
    }
}