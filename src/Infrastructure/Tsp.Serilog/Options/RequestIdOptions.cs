namespace Tsp.Serilog.Options
{
    public class RequestIdOptions
    {
        public bool? Enable { get; set; }
        /// <summary>
        /// The header field name where the request ID will be stored
        /// </summary>
        public string HeaderName { get; set; } = "X-Request-Id";

        /// <summary>
        /// Controls whether the correlation ID is returned in the response headers
        /// </summary>
        public bool IncludeInResponse { get; set; } = true;

        public string PropertyName { get; set; } = "RequestId";
    }
}
