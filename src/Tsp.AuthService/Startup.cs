using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Tsp.Authorization.DI;
using Tsp.Authorization.JWT;
using Tsp.ExceptionHandling;
using Tsp.ExceptionHandling.Exceptions;
using Tsp.HealthCheck;
using Tsp.Swagger;
using Tsp.Swagger.Extensions;

namespace Tsp.AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            services.AddHttpClient();

            services.AddControllers()
                .AddJsonOptions(json =>
                        {
                            json.JsonSerializerOptions.WriteIndented = true;
                        }
                );

            services.AddBaseHealthChecks();
            services.AddSwaggerDocumentation(() => new Dictionary<string, SwaggerOptions> 
            {
                { "auth", new SwaggerOptions
                        {
                            Version = "v1",
                            Name = "Auth api",
                            Title = "Authorization Service",
                            ParameterLocation = ParameterLocation.Header,
                            EnableBearerAuth = false,

                        }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
            app.UseSwaggerDocumentation("auth");
        }
    }
}
