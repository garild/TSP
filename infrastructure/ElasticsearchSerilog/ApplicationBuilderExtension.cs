using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ElasticsearchSerilog
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSerilogRequestLogging(this IApplicationBuilder applicationBuilder, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
                applicationBuilder.UseSerilogRequestLogging();
        }
    }
}
