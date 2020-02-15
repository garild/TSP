using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Tsp.HealthCheck.ConstantServices
{
    public class ElasticsearchHealthCheck : IHealthCheck
    {
        private const string AUTH_HEADER_TYPE = "Basic";
        private const string ELASTICSEARCH_USERNAME = "elastic";

        private readonly IHttpClientFactory _clientFactory;

        public ElasticsearchHealthCheck(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var elasticserachHostAddress = string.Empty;
            var elasticsearchPassword = string.Empty;
            try
            {
                elasticserachHostAddress = Environment.GetEnvironmentVariable("ELASTICSEARCH_HOSTS") ??
                    throw new ArgumentNullException("Missing 'ELASTICSEARCH_HOSTS' environment variable .");

                elasticsearchPassword = Environment.GetEnvironmentVariable("ELASTICSEARCH_PASSWORD") ??
                    throw new ArgumentNullException("Missing 'ELASTICSEARCH_PASSWORD' environment variable .");

                var authValue = new AuthenticationHeaderValue(AUTH_HEADER_TYPE, Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ELASTICSEARCH_USERNAME}:{elasticsearchPassword}")));

                var request = new HttpRequestMessage(HttpMethod.Get, elasticserachHostAddress);

                var client = _clientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);

                client.DefaultRequestHeaders.Authorization = authValue;

                var response = await client.SendAsync(request, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"HealthCheck request to ES endpoint {elasticserachHostAddress} failed with error message [{ex.Message}]");
            }

            return HealthCheckResult.Healthy();
        }
    }
}