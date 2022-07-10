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
    [Route("api/[controller]s")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IApiGatewayService _apiGatewayService;

        public GameController(IApiGatewayService apiGatewayService)
        {
            _apiGatewayService = apiGatewayService;
        }

        [HttpGet]
        public async Task<GameGetDto> Post()
        {
            var result = await _apiGatewayService.CreateGame(new GamePostDto());
            return result;
        }
        
        
        
    }
    
}