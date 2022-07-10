using MatchProtocol.Platform.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MatchProtocol.Platform.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMatchProtocolServicePlatform(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}