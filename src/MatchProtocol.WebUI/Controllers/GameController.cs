using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MatchProtocol.DTO.Games;
using MatchProtocol.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatchProtocol.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IApiGatewayService _apiGatewayService;

        public GameController(IApiGatewayService apiGatewayService)
        {
            _apiGatewayService = apiGatewayService;
        }

        [HttpPost]
        public async Task<GameGetDto> Post(GamePostDto dto)
        {
            var result = await _apiGatewayService.CreateGame(dto);
            return result;
        }
        
        [HttpGet("{gameId}")]
        public async Task<GameGetDto> Get(int gameId)
        {
            var result = await _apiGatewayService.GetGame(gameId);
            return result;
        }
        
        
        
        
        
    }
    
}