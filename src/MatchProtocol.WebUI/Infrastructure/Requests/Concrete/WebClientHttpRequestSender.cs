using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MatchProtocol.WebUI.Infrastructure.Requests.Abstract;
using Newtonsoft.Json;

namespace MatchProtocol.WebUI.Infrastructure.Requests.Concrete
{
    public class WebClientHttpRequestSender : IWebClientHttpRequestSender
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebClientHttpRequestSender(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<T> GetAsync<T>(string url)
        {
            return await SendAsync<T>(url, HttpMethod.Get);
        }
        
        public async Task<T> PostAsync<T>(string url, object body)
        {
            return await SendAsync<T>(url, HttpMethod.Post, body);
        }

        private async Task<T> SendAsync<T>(string url, HttpMethod method, object body = null)
        {
            HttpClient client = _httpClientFactory.CreateClient("apiGatewayClient");

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

        
    }
}