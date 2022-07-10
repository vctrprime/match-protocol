using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchProtocol.Platform.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MatchProtocol.GameSettings
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var policies = new Dictionary<string, string[]>
            {
                { "getWeatherPolicy", new [] { "games.service" } }
            };
            
            services.UseMatchProtocolServicePlatform(
                Configuration,
                "https://localhost:9005", 
                "game-settings-service",
                "game-settings-service-secret",
                policies);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMatchProtocolServicePlatform();
        }
    }
}