using System.Threading.Tasks;

namespace MatchProtocol.WebUI.Infrastructure.Requests.Abstract
{
    public interface IWebClientHttpRequestSender
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object body);
    }
}