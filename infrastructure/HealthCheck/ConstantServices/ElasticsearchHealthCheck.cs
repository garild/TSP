using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.ConstantServices
{
    public class ElasticsearchHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;

        public ElasticsearchHealthCheck(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var elasticserachHostAddress = string.Empty;
            try
            {
                elasticserachHostAddress = Environment.GetEnvironmentVariable("ELASTICSEARCH_HOSTS") ?? throw new ArgumentNullException("Missing 'ELASTICSEARCH_HOSTS' environment variable .");

                var request = new HttpRequestMessage(HttpMethod.Get, elasticserachHostAddress);

                var client = _clientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"HealthCheck request to ES endpoint {elasticserachHostAddress}, caused error .{ex.Message}");
            }

            return HealthCheckResult.Healthy();
        }
    }
}
