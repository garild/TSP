using System.Threading;

namespace Tsp.Cqrs
{
    public class StandardGate : IGate
    {
        private readonly IRunEnvironment _runEnvironment;

        public StandardGate(IRunEnvironment environment)
        {
            _runEnvironment = environment;
        }

        public void RunCommand<T>(T command, CancellationToken cancellationToken)
        {
            _runEnvironment.RunCommand(command, cancellationToken);
        }

        public TOut RunCommand<T, TOut>(T command, CancellationToken cancellationToken)
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