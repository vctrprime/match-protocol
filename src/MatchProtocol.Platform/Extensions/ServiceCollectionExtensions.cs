using System;
using System.Collections.Generic;
using System.Net.Http;
using MatchProtocol.Platform.Handlers;
using MatchProtocol.Platform.Requests.Abstract;
using MatchProtocol.Platform.Requests.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace MatchProtocol.Platform.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseMatchProtocolServicePlatform(this IServiceCollection services, 
            IConfiguration configuration,
            string identityServerUrl, string clientId, string clientSecret,
            IDictionary<string, string[]> policies = null)
        {
           
            services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
            services.AddTransient<RequestDelegatingHandler>();
            services.AddHttpContextAccessor();
            
            services.AddHttpClient("serviceHttpClient", client =>
            { 
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<RequestDelegatingHandler>();
            
            services.AddTransient<IHttpRequestSender>(x => 
                new HttpRequestSender(x.GetRequiredService<IHttpClientFactory>(),
                    identityServerUrl, 
                    clientId,
                    clientSecret));
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityServerUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("webClientPolicy", policy => policy.RequireClaim("client_id", "match-protocol-web-client"));
                if (policies == null) return;
                
                foreach (var (key,value) in policies)
                {
                    options.AddPolicy(key, policy => policy.RequireClaim("client_id", value));
                }


            });
            
            return services;
        }
    }
}