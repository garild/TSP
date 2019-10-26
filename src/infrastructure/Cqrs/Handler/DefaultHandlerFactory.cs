using System;
using Cqrs.Handler.Queries;
using Infrastructure.Cqrs.Handler.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cqrs.Handler
{
    internal class DefaultHandlerFactory : ICommandHandlerFactory, IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<TCommand> CreateCommand<TCommand>()
        {
            return _serviceProvider.GetService<ICommandHandler<TCommand>>();
        }

        public ICommandHandler<TCommand, TResult> CreateCommand<TCommand, TResult>()
        {
            return _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
        }

        public IQueryHandler<TQuery, TResponse> CreateQuery<TQuery, TResponse>()
        {
            return _serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>();
        }

        public IQueryHandler<TResponse> CreateQuery<TResponse>()
        {
            return _serviceProvider.GetService<IQueryHandler<TResponse>>();
        }
    }
}