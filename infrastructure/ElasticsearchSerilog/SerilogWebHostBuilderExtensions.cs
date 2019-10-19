using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;

namespace ElasticsearchSerilog
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static void UseSerilog(this IWebHostBuilder webBuilder, LogEventLevel overideMicrosoftLogLevel = LogEventLevel.Warning)
        {
            webBuilder.UseSerilog((ctx, config) =>
            {
                if (ctx.HostingEnvironment.IsProduction() || ctx.HostingEnvironment.IsStaging())
                {
                    config.MinimumLevel.Override("Microsoft", overideMicrosoftLogLevel);
                    config.WriteTo.Console(new ElasticsearchJsonFormatter());
                }
                else
                {
                    config.MinimumLevel.Information().Enrich.FromLogContext();
                    config.WriteTo.Console();
                }
            });
        }
    }
}
