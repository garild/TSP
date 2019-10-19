using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HealthCheck
{
    public static class ApplicationBuilderExtension
    {
        public static void UseHealthCheck(this IApplicationBuilder application)
        {
            var options = new HealthCheckOptions();
            options.ResultStatusCodes[HealthStatus.Unhealthy] = 503;
            options.ResponseWriter = WriteResponse;

            options.AllowCachingResponses = false;
            options.Predicate = _ => true;

            application.UseEndpoints(endpoints => {
                endpoints.MapHealthChecks("/hc", options);
            });
        }

        private static Task WriteResponse(HttpContext httpContext, HealthReport hr)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;

            var result = JsonConvert.SerializeObject(
               new
               {
                   name = "[WebApi] Auth Service",
                   status = hr.Status.ToString(),
                   services = hr.Entries.Select(e =>
                   new
                   {
                       description = e.Key,
                       status = e.Value.Status.ToString(),
                       tag = e.Value.Tags,
                       errorMessage = e.Value.Exception?.Message ?? e.Value.Exception?.InnerException.Message ?? e.Value.Description,
                       responseTime = e.Value.Duration.TotalMilliseconds
                   }),
                   totalResponseTime = hr.TotalDuration.TotalMilliseconds,

               }, Formatting.Indented); ; ;

            return httpContext.Response.WriteAsync(result);
        }
    }
}
