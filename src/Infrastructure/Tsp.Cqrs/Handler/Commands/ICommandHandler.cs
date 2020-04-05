using System.Threading;

namespace Tsp.Cqrs.Handler.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
    {
        void Handle(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<TCommand, TOut> : ICommandHandler
    {
        TOut Handle(TCommand command, CancellationToken cancellationToken = default);
    }
}