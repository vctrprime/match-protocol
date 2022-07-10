using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MatchProtocol.Platform.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;  
  
        public LoggingMiddleware(RequestDelegate next)  
        {  
            this._next = next;  
        }  
  
        public async Task InvokeAsync(HttpContext context)  
        {  
            var header = context.Request.Headers["Correlation-Token"];

            var correlationToken = header.Any() ? header[0] : Guid.NewGuid().ToString();  
  
            context.Items["Correlation-Token"] = correlationToken;
            var logger = context.RequestServices.GetRequiredService<ILogger<LoggingMiddleware>>();  
            using (logger.BeginScope("{@CorrelationToken}", correlationToken))  
            {  
                await this._next(context);  
            }
        }  
    }
}