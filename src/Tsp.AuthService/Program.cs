using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ElasticsearchSerilog;
using Serilog.Events;
using System;

namespace Tsp.AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS"));
                });

    }
}
