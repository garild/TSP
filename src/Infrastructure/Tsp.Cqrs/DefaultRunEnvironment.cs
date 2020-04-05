using System;
using System.Threading;
using Tsp.Cqrs.Handler.Commands;
using Tsp.Cqrs.Handler.Queries;

namespace Tsp.Cqrs
{
    internal class DefaultRunEnvironment : IRunEnvironment
    {
        private readonly ICommandHandlerFactory _commandFactory;
        private readonly IQueryHandlerFactory _queryFactory;

        public DefaultRunEnvironment(ICommandHandlerFactory commandFactory,  IQueryHandlerFactory queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
        }

        public void RunCommand<T>(T command, CancellationToken cancellationToken = default)
        {
            var handler = _commandFactory.CreateCommand<T>();
            handler.Handle(command, cancellationToken);
        }

        public TOut RunCommand<T, TOut>(T command, CancellationToken cancellationToken = default)
        {
            var handler = _commandFactory.CreateCommand<T, TOut>();
            var result = handler.Handle(command, cancellationToken);
            return result;
        }

        public TResponse RunQuery<TQuery, TResponse>(TQuery query)
        {
            var handler = _queryFactory.CreateQuery<TQuery, TResponse>();

            if (handler == null)
            {
                throw new ArgumentNullException($"Can't find handler for query type {typeof(TQuery).FullName}");
            }

            var result = handler.Handle(query);

            return result;
        }

        public TResponse RunQuery<TResponse>()
        {
            var handler = _queryFactory.CreateQuery<TResponse>();

            if (handler == null)
            {
                throw new ArgumentNullException($"Can't find handler for query which has return type {typeof(TResponse).FullName}");
            }

            var result = handler.Handle();

            return result;
        }
    }
}