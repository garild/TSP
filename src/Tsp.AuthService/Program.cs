using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Tsp.Serilog.Extensions;

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
                    webBuilder.UseSerilog(p =>
                    {
                        p.ClearProviders = true;
                        p.WriteToFile = true;
                        p.UseEvniromentVariables = true;
                    });
                    webBuilder.UseUrls(Environment.GetEnvironmentVariable("ASPNETCORE_URLS"));
                });

    }
}
