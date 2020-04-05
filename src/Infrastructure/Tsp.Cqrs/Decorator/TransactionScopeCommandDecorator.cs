using System.Threading;
using System.Transactions;

namespace Tsp.Cqrs.Decorator
{
    internal class TransactionScopeCommandDecorator : IRunEnvironment
    {
        private readonly IRunEnvironment _runEnvironment;

        public TransactionScopeCommandDecorator(IRunEnvironment runEnvironment)
        {
            _runEnvironment = runEnvironment;
        }

        public TResult RunCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                return _runEnvironment.RunCommand<TCommand, TResult>(command, cancellationToken);
            }
        }

        public void RunCommand<T>(T command, CancellationToken cancellationToken = default)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _runEnvironment.RunCommand(command, cancellationToken);
            }
        }

        public TResponse RunQuery<TQuery, TResponse>(TQuery query)
        {
            return _runEnvironment.RunQuery< TQuery,TResponse>(query);
        }

        public TResponse RunQuery<TResponse>()
        {
            return _runEnvironment.RunQuery<TResponse>();
        }
    }
}