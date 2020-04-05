using System.Threading;

namespace Tsp.Cqrs.Decorator
{
    internal class RunEnvironmentDecorator : IRunEnvironment
    {
        private readonly IRunEnvironment _runEnvironment;

        public RunEnvironmentDecorator(IRunEnvironment runEnvironment)
        {
            _runEnvironment = runEnvironment;
        }

        public void RunCommand<T>(T command, CancellationToken cancellationToken = default)
        {
            _runEnvironment.RunCommand(command, cancellationToken);
        }

        public TOut RunCommand<T, TOut>(T command, CancellationToken cancellationToken = default)
        {
            return _runEnvironment.RunCommand<T, TOut>(command, cancellationToken);
        }

        public TResponse RunQuery<TQuery, TResponse>(TQuery query)
        {
            return _runEnvironment.RunQuery<TQuery, TResponse>(query);
        }

        public TResponse RunQuery<TResponse>()
        {
            return _runEnvironment.RunQuery<TResponse>();
        }
    }
}