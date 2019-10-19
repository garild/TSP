using System.Text.Json.Serialization;

namespace ExceptionHandling
{
    public class ExceptionDetails
    {
        [JsonIgnore]
        public int StatusCode { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
