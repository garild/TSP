using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ElasticsearchSerilog
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSerilogRequestLogger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSerilogRequestLogging();
        }
    }
}
