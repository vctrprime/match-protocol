using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace MatchProtocol.Platform.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder) =>
            builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                //.ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {CorrelationToken} {Message:lj}{NewLine}{Exception}"));
    }
}