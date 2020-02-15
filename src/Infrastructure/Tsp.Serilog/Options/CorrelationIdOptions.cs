namespace Tsp.Serilog.Options
{
    public class CorrelationIdOptions
    {
        public bool? Enable { get; set; } = false;
        /// <summary>
        /// The header field name where the correlation ID will be stored
        /// </summary>
        public string HeaderName { get; set; } = "X-Correlation-Id";

        /// <summary>
        /// Controls whether the correlation ID is returned in the response headers
        /// </summary>
        public bool IncludeInResponse { get; set; } = true;

        public string PropertyName { get; set; } = "CorrelationId";
    }
}
