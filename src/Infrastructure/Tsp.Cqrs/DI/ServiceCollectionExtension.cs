using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tsp.Cqrs.Decorator;
using Tsp.Cqrs.Handler;
using Tsp.Cqrs.Handler.Commands;
using Tsp.Cqrs.Handler.Queries;

namespace Tsp.Cqrs.DI
{
    public static class ServiceCollectionExtension
    {
        public static void AddCqrs(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var loadedAssemblies = assemblies.Any() ? assemblies : FindReferencedAssemblies();

            serviceCollection.Scan(scan =>
                scan.FromAssemblies(loadedAssemblies).AddClasses(classes => classes.AssignableTo<ICommandHandler>())
                    .AsImplementedInterfaces().WithScopedLifetime());

            serviceCollection.Scan(scan =>
              scan.FromAssemblies(loadedAssemblies).AddClasses(classes => classes.AssignableTo<IQueryHandler>())
                  .AsImplementedInterfaces().WithScopedLifetime());

            serviceCollection.AddScoped<ICommandHandlerFactory, DefaultHandlerFactory>();
            serviceCollection.AddScoped<IQueryHandlerFactory, DefaultHandlerFactory >();
            serviceCollection.AddScoped<IGate, StandardGate>();

            serviceCollection.AddScoped<IRunEnvironment, DefaultRunEnvironment>();
            serviceCollection.Decorate<IRunEnvironment, RunEnvironmentDecorator>();

            serviceCollection.Decorate<IRunEnvironment>(inner => new TransactionScopeCommandDecorator(inner));
            serviceCollection.Decorate<IRunEnvironment>((inner, provider) =>
                new RequestLoggerDecorator(inner,
                    provider.GetService<ILoggerFactory>()));
        }

        private static Assembly[] FindReferencedAssemblies()
        {
            var referencedAssembliesNames = Assembly.GetEntryAssembly()?.GetReferencedAssemblies().Select(p => p.Name).ToList();

            return referencedAssembliesNames?.Select(Assembly.Load).ToArray();
        }
    }
}