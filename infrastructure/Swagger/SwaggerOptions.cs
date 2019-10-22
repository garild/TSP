using Microsoft.OpenApi.Models;
using System;

namespace Swagger
{
    public class SwaggerOptions : OpenApiInfo
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string RoutePrefix { get; set; }
        public bool EnableBearerAuth { get; set; }

        public SecuritySchemeType SecurityScheme { get; set; }
        public ParameterLocation ParameterLocation { get; set; }
    }
}
