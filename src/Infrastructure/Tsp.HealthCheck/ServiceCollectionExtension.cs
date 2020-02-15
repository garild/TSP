using Microsoft.Extensions.DependencyInjection;
using Tsp.HealthCheck.ConstantServices;

namespace Tsp.HealthCheck
{
    public static class ServiceCollectionExtension
    {
        public static void AddBaseHealthChecks(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHealthChecks()
            .AddCheck<FluentdHealthCheck>("Fluentd")
            //TODO Export as ASPNET CORE ENV
            .AddCheck<ElasticsearchHealthCheck>("Elasticsearch");
        }
    }
}
