using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Swagger.Extensions
{
    public static class SwaggerServiceExtensions
    {
        private const string HEADER_AUTH_NAME = "Authorization";

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, Func<IDictionary<string, SwaggerOptions>> setupSwaggerDocActions)
        {
            services.AddMvcCore()
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(swagger =>
                {
                    foreach (var swaggerDoc in setupSwaggerDocActions.Invoke())
                    {
                        if (swaggerDoc.Value.Enabled)
                        {
                            swagger.DescribeAllParametersInCamelCase();
                            swagger.SwaggerDoc(swaggerDoc.Key, swaggerDoc.Value);
                            
                            swagger.CustomSchemaIds(x => x.FullName);
                           
                            if (swaggerDoc.Value.EnableBearerAuth)
                            {
                                var openApiSecurity = new OpenApiSecurityScheme
                                {
                                    Description =
                                       "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                    Name = HEADER_AUTH_NAME,
                                    In = swaggerDoc.Value.ParameterLocation,
                                    Type = swaggerDoc.Value.SecurityScheme
                                };
                                swagger.AddSecurityDefinition("Bearer", openApiSecurity);
                            }
                        }
                    }
                }
            );

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app,string apiName)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "{documentName}/api/swagger.json";
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.EnableDeepLinking();
                c.DefaultModelExpandDepth(-1);
                c.SwaggerEndpoint($"/{apiName}/api/swagger.json", apiName);
            });

            return app;
        }
    }
}

