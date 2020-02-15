using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tsp.ExceptionHandling.Exceptions.Abstractions;

namespace Tsp.ExceptionHandling.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _request;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate request, ILoggerFactory loggerFactory)
        {
            _request = request;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _request(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errorResponse = new ExceptionDetails();

            if (exception is IDomainException ex)
            {
                errorResponse.Code = ex.Code;
                errorResponse.Message = ex.Message;
                errorResponse.StatusCode = ex.HttpStatusCode;
            }
            else
            {
                errorResponse.Code = "InternalServerError";
                errorResponse.StatusCode = 500;
                errorResponse.Message = exception.Message;
            }

            var result = JsonSerializer.Serialize(errorResponse);

            httpContext.Response.ContentType = JsonContentType;
            httpContext.Response.StatusCode = errorResponse.StatusCode;

            return httpContext.Response.WriteAsync(result);
        }
    }
}
