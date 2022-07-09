using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatchProtocol.WebUI.Controllers
{
    [ApiController]
    [Route("teamB")]
    [Authorize]
    public class TeamWithCheckTokenController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        

        [HttpGet]
        public async Task<IEnumerable<Team>> Get()
        {
            ////////////////////////// //////////////////////// ////////////////////////
            //// WAY 2 :

            //// 1. "retrieve" our api credentials. This must be registered on Identity Server!
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:9005/connect/token",

                ClientId = "another-service",
                ClientSecret = "secret1",

                // This is the scope our Protected API requires. 
                Scope = "teams.service"
            };

            //// creates a new HttpClient to talk to our IdentityServer (localhost:5005)
            var client = new HttpClient();

            //// just checks if we can reach the Discovery document. Not 100% needed but..
            /*var disco = await client.GetDiscoveryDocumentAsync("https://localhost:9005");
            if (disco.IsError)
            {
                return null; // throw 500 error
            }*/

            //// 2. Authenticates and get an access token from Identity Server
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);            
            if (tokenResponse.IsError)
            {
                return null;
            }

            //// Another HttpClient for talking now with our Protected API
            var apiClient = new HttpClient();

            //// 3. Set the access_token in the request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            //// 4. Send a request to our Protected API
            var response = await apiClient.GetAsync("https://localhost:9015/team/B");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var movieList = JsonConvert.DeserializeObject<List<Team>>(content);
            return movieList;
        }
    }
}