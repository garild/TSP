using Serilog.Events;

namespace Tsp.Serilog.Options
{
    public class RequestContextOptions
    {
        public LogEventLevel LogLevel { get; set; } = LogEventLevel.Information;
        public CorrelationIdOptions CorrelationId { get; set; } = new CorrelationIdOptions();
        public RequestIdOptions RequestId { get; set; } = new RequestIdOptions();

        /// <summary>
        /// <seealso cref="ExceptionHandlerMiddleware"/>
        /// </summary>
        public bool EnableExceptionMiddleware { get; set; }
    }
}
