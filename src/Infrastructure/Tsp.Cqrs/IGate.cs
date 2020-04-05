using System.Threading;

namespace Tsp.Cqrs
{
    public interface IGate
    {
        void RunCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default);

        TResult RunCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default);

        TResponse RunQuery<TQuery, TResponse>(TQuery query);

        TResponse RunQuery<TResponse>();
    }
}