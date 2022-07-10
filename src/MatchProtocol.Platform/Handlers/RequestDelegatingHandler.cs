using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MatchProtocol.Platform.Handlers
{
    public class RequestDelegatingHandler : DelegatingHandler
        {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public RequestDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            var correlationToken = context?.Items["Correlation-Token"] as string;
            request.Headers.Add("Correlation-Token", correlationToken);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}