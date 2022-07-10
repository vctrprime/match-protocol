using System.Threading.Tasks;
using MatchProtocol.DTO.Games;
using MatchProtocol.WebUI.Infrastructure.Requests.Abstract;
using MatchProtocol.WebUI.Services.Abstract;

namespace MatchProtocol.WebUI.Services.Concrete
{
    public class ApiGatewayService : IApiGatewayService
    {
        private readonly IWebClientHttpRequestSender _httpRequestSender;

        public ApiGatewayService(IWebClientHttpRequestSender httpRequestSender)
        {
            _httpRequestSender = httpRequestSender;
        }
        
        public async Task<GameGetDto> CreateGame(GamePostDto game)
        {
            var gameResult = await _httpRequestSender.PostAsync<GameGetDto>("api/games", game);
            return gameResult;
        }
    }
}