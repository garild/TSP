using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Formatting.Elasticsearch;
using Serilog.Events;
using Microsoft.Extensions.Hosting;

namespace ElasticsearchSerilog
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static void UseSerilog(this IWebHostBuilder webBuilder, LogEventLevel minimumLevelLog = LogEventLevel.Information, LogEventLevel overideMicrosoftLogLevel = LogEventLevel.Warning)
        {
            webBuilder.UseSerilog((ctx, config) =>
            {
               
                    if (ctx.HostingEnvironment.IsProduction() || ctx.HostingEnvironment.IsStaging())
                    {
                        config.MinimumLevel.Override("Microsoft", overideMicrosoftLogLevel);

                        if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("FLUENTD_LOG_PATH")))
                            config.WriteTo.Console(new ElasticsearchJsonFormatter());
                        else
                        {
                            config.ReadFrom.Configuration(ctx.Configuration).Enrich.FromLogContext().WriteTo.File(
                            new JsonFormatter(),
                            System.Environment.GetEnvironmentVariable("FLUENTD_LOG_PATH"),
                            rollingInterval: RollingInterval.Day,
                            shared: true,
                            flushToDiskInterval: System.TimeSpan.FromSeconds(1)
                            );
                        }
                    }

                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        config.MinimumLevel.Information().Enrich.FromLogContext();
                        config.WriteTo.Console();
                    }

                });
        }
    }
}
