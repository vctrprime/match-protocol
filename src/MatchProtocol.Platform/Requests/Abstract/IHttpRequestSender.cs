using System.Threading.Tasks;

namespace MatchProtocol.Platform.Requests.Abstract
{
    public interface IHttpRequestSender
    {
        Task<T> GetAsync<T>(string url, string scope);
    }
}