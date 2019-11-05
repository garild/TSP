using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.ConstantServices
{
    public class FluentdHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _clientFactory;

        public FluentdHealthCheck(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var fluentdHostAddress = string.Empty;

            try
            {
                fluentdHostAddress = Environment.GetEnvironmentVariable("FLUENTD_HOST") ?? throw new ArgumentNullException("Missing 'FLUENTD_HOST' environment variable .");

                var request = new HttpRequestMessage(HttpMethod.Get, fluentdHostAddress);

                var client = _clientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(5);

                var response = await client.SendAsync(request, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"HealthCheck request to Fluentd endpoint {fluentdHostAddress} failed with error message [{ex.Message}]");
            }

            return HealthCheckResult.Healthy();
        }
    }
}
