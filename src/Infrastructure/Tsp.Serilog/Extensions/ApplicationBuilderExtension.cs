using System;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Tsp.Serilog.Options;

namespace Tsp.Serilog.Extensions
{
    public static class ApplicationBuilderExtension
    {
        const string LogMessageTemplate = "Handled Request {RequestId} for HTTP {RequestMethod} {RequestScheme}://{RequestHost}{RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        public static void UseSerilogRequestContextLogging(this IApplicationBuilder app, Action<RequestContextOptions> options = default)
        {
            var requestOptions = new RequestContextOptions();
            options?.Invoke(requestOptions);

            var addRequestId = requestOptions.RequestId?.Enable ?? false;

            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = IncludeRequestId(addRequestId,LogMessageTemplate);

                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => requestOptions.LogLevel;

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                    diagnosticContext.Set("RequestPath", httpContext.Request.Path);

                    if(addRequestId)
                        diagnosticContext.Set("RequestId", httpContext.Request.Headers.ContainsKey("RequestId") ? httpContext.Request.Headers?["RequestId"].ToString() : string.Empty);
                };
            });
        }

        private static Func<bool?, string, string> IncludeRequestId => (requestId, message)
              => requestId.GetValueOrDefault(false) ? message : message.Replace("{RequestId} ", string.Empty);
    }
}
