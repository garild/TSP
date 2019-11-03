using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ElasticsearchSerilog
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSerilogRequestLogger(this IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            applicationBuilder.UseSerilogRequestLogging();
        }
    }
}
