using HealthCheck.ConstantServices;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheck
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
