using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using MatchProtocol.Platform.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MatchProtocol.WebUI.Infrastructure.Handlers
{
    public class WebClientRequestDelegatingHandler : RequestDelegatingHandler
    {

        public WebClientRequestDelegatingHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            var accessToken = await context?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)!;
            
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}