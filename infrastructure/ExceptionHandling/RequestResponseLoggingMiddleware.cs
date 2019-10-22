using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ExceptionHandling
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly string Regex_Ingore_Route = @"\.(.*)?";
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var regexMatch = Regex.Match(context.Request.Path.Value, Regex_Ingore_Route);
            if (regexMatch.Success)
            {
                await _next(context);
                return;
            }

            var contextGuid = Guid.NewGuid();

            var request = await FormatRequest(context.Request, contextGuid);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                var response = await FormatResponse(context.Response, contextGuid);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request, Guid requestId)
        {

            request.EnableBuffering();

            if (request.ContentLength == null && !request.Body.CanSeek)
            {
                return null;
            }

            var bufferLength = request.ContentLength ?? request.Body.Length;

            var buffer = new byte[Convert.ToInt32(bufferLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            var bodyString = $"{request.Scheme}://{request.Host}{request.Path} QueryString:{request.QueryString} Body:{bodyAsText}";

            if (!string.IsNullOrEmpty(bodyString))
                _logger.LogInformation($"Incomming {requestId} request {bodyString}");

            return bodyAsText;
        }

        private async Task<string> FormatResponse(HttpResponse response, Guid responseId)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string responseString = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Request {responseId} ended with status {response.StatusCode}, response body {responseString}");

            return responseString;
        }
    }
}
