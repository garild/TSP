using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Formatting.Elasticsearch;
using Serilog.Events;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ElasticsearchSerilog
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static void UseSerilog(this IWebHostBuilder webBuilder, LogEventLevel minimumLevelLog = LogEventLevel.Information, LogEventLevel overideMicrosoftLogLevel = LogEventLevel.Warning)
        {
            webBuilder.UseSerilog((ctx, config) =>
            {
                config.ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.FromLogContext()
                    .MinimumLevel.Is(minimumLevelLog)
                    .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ServiceId", System.Environment.GetEnvironmentVariable("SERVICE_POD_NAME"));

                if (ctx.HostingEnvironment.IsProduction() || ctx.HostingEnvironment.IsStaging())
                {
                    config.MinimumLevel.Override("Microsoft", overideMicrosoftLogLevel);

                    //TODO  REMOVE THIS after k8s will be deployed
                    if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("FLUENTD_LOG_PATH")))
                        config.WriteTo.Console(new ElasticsearchJsonFormatter());
                    else
                    {
                        config.WriteTo.File(
                        new JsonFormatter(),
                        Path.Combine(System.Environment.GetEnvironmentVariable("FLUENTD_LOG_PATH"), $"{System.Environment.GetEnvironmentVariable("SERVICE_NAME")}_.log"),
                        rollingInterval: RollingInterval.Day,
                        shared: true,
                        flushToDiskInterval: System.TimeSpan.FromSeconds(1)
                        );

                        config.WriteTo.Console();
                    }
                }

                if (ctx.HostingEnvironment.IsDevelopment())
                {
                    config.MinimumLevel.Information();
                    config.WriteTo.Console();
                }
            });
        }
    }
}
