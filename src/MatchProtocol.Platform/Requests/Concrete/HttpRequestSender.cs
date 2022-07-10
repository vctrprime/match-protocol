using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using MatchProtocol.Platform.Handlers;
using MatchProtocol.Platform.Requests.Abstract;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MatchProtocol.Platform.Requests.Concrete
{
    public class HttpRequestSender : IHttpRequestSender
    {
        private readonly string _identityServerUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpRequestSender(IHttpClientFactory httpClientFactory, string identityServerUrl, string clientId, string clientSecret)
        {
            _httpClientFactory = httpClientFactory;
            _identityServerUrl = identityServerUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<T> GetAsync<T>(string url, string scope)
        {
            return await SendAsync<T>(url, HttpMethod.Get, scope);
        }

        private async Task<T> SendAsync<T>(string url, HttpMethod method, string scope, object body = null)
        {
            HttpClient client = await CreateHttpClient(scope, url);

            var request = new HttpRequestMessage(
                method,
                url);
            
            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body);

                var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

                request.Content = requestBody;
            }
            
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        private async Task<HttpClient> CreateHttpClient(string scope, string url)
        {
            var serviceClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = $"{_identityServerUrl}/connect/token",

                ClientId = _clientId,
                ClientSecret = _clientSecret,
                
                Scope = scope
            };

            var client = _httpClientFactory.CreateClient("serviceHttpClient"); 
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(serviceClientCredentials);            
            if (tokenResponse.IsError)
            {
                throw new SecurityException($"Запрещен доступ к ресурсу: {url}!");
            }
            
            var serviceClient = _httpClientFactory.CreateClient("serviceHttpClient");
            serviceClient.SetBearerToken(tokenResponse.AccessToken);

            return serviceClient;

        }
    }
}