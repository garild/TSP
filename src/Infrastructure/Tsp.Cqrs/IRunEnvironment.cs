using System.Threading;

namespace Tsp.Cqrs
{
    public interface IRunEnvironment
    {
        void RunCommand<T>(T command, CancellationToken cancellationToken = default);
        TOut RunCommand<T, TOut>(T command, CancellationToken cancellationToken = default);

        TResponse RunQuery<TQuery, TResponse>(TQuery query);

        TResponse RunQuery<TResponse>();
    }
}