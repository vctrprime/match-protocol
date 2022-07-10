using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatchProtocol.WebUI.Controllers
{
    [ApiController]
    [Route("teamA")]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TeamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> Get()
        {
            var httpClient = _httpClientFactory.CreateClient("teams.service");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/team/A");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Team>>(content);
            return movieList;
        }
        
        [HttpGet("A")]
        public async Task<IEnumerable<Team>> GetAA()
        {
            var httpClient = _httpClientFactory.CreateClient("teams.service");

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/team/B");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Team>>(content);
            return movieList;
        }
        
    }
    
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}