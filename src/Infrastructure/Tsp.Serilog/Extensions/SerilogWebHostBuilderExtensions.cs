using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using Tsp.Serilog.Options;

namespace Tsp.Serilog.Extensions
{
    public static class SerilogWebHostBuilderExtensions
    {
        public static void UseSerilog(this IWebHostBuilder webBuilder, Action<LoggerConfigurationOptions>  config = default)
        {
            var loggerOptions = new LoggerConfigurationOptions();
            config?.Invoke(loggerOptions);

            if (loggerOptions.ClearProviders)
                webBuilder.ConfigureLogging(logger => logger.ClearProviders());

            webBuilder.UseSerilog((ctx, loggerConfiguration) =>
            {
                var options = ctx.Configuration.GetOptions<SerilogOptions>(loggerOptions.SectionName);

                Configure(options, loggerConfiguration, loggerOptions, ctx);

            }, writeToProviders: true);
           
        }
        private static void Configure(SerilogOptions options, LoggerConfiguration config, LoggerConfigurationOptions configurationOptions, WebHostBuilderContext ctx)
        {
           if(configurationOptions.UseEvniromentVariables)
            {
                options.ServiceName = TryGetEnvironmentVariable("SERVICE_POD_NAME");
                options.FilePath = TryGetEnvironmentVariable("SERILOG_LOG_PATH");
            }
            else
                ValidateOptions(new[] { options?.FilePath, options?.ServiceName });
           
            config
                .MinimumLevel.Override("Microsoft.AspNetCore", configurationOptions.OverrideDefaultLevelLog)
                .MinimumLevel.Is(configurationOptions.MinimumLevelLog)
                .Enrich.WithProperty("ServiceId", options.ServiceName)
                .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                .Enrich.FromLogContext()
                .WriteTo.Console();

            if (!configurationOptions.WriteToFile) return;

            var fileLogName = $"{options.ServiceName}_.log";

            config.WriteTo.File(
                           new JsonFormatter(renderMessage: false),
                            Path.Combine(options.FilePath, fileLogName),
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            retainedFileCountLimit: 5,
                            fileSizeLimitBytes: 500_000_000,
                            shared: true,
                            //outputTemplate: options.OutputTemplate,
                            flushToDiskInterval: TimeSpan.FromSeconds(1)
                           );

        }
        private static Action<string[]> ValidateOptions => (data) =>
        {
            if (data.Any(string.IsNullOrEmpty))
                throw new ArgumentNullException("Invalid configuration of serilog section .");
        };

        private static string TryGetEnvironmentVariable(string variable) =>
            string.IsNullOrEmpty(variable)
            ? throw new ArgumentNullException(variable, "Enviroment variable cannot be null .")
            : Environment.GetEnvironmentVariable(variable);
    }
}
