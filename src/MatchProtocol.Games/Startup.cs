using MatchProtocol.Games.Services.Abstract;
using MatchProtocol.Games.Services.Concrete;
using MatchProtocol.Platform.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MatchProtocol.Games
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
            
            services.UseMatchProtocolServicePlatform(
                Configuration,
                "https://localhost:9005", 
                "games.service",
                "games.service-secret");

            services.AddTransient<IGameSettingsApiService, GameSettingsApiService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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