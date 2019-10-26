using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Microsoft.Extensions.Hosting;

namespace ElasticsearchSerilog
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static void UseSerilog(this IWebHostBuilder webBuilder, LogEventLevel minimumLevelLog = LogEventLevel.Information, LogEventLevel overideMicrosoftLogLevel = LogEventLevel.Warning)
        {
            webBuilder.UseSerilog((ctx, config) =>
            {
                var env = ctx.HostingEnvironment.EnvironmentName;

                if (ctx.HostingEnvironment.IsProduction() || ctx.HostingEnvironment.IsStaging())
                {
                    config.MinimumLevel.Override("Microsoft", overideMicrosoftLogLevel);
                    config.WriteTo.Console(new ElasticsearchJsonFormatter());
                }
                else
                {
                    config.MinimumLevel.ControlledBy(new LoggingLevelSwitch(minimumLevelLog)).Enrich.FromLogContext();
                    config.WriteTo.Console();
                }
            });
        }
    }
}
