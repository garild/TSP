using Auth.DI;
using Auth.JWT;
using ElasticsearchSerilog;
using ExceptionHandling;
using ExceptionHandling.Exceptions;
using HealthCheck;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tsp.AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
              .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
    
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                   builder =>
                   {
                       builder.SetIsOriginAllowedToAllowWildcardSubdomains();

                   });
            });

            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));

            services.AddAuthJwt(Configuration);

            services.AddControllers()
                .AddJsonOptions(json =>
                        {
                            json.JsonSerializerOptions.WriteIndented = true;
                        }
                );

            services.AddBaseHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseSerilogRequestLogger();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseRouting();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthCheck();
        }
    }
}
