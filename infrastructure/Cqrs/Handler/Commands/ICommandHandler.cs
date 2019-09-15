namespace Infrastructure.Cqrs.Handler.Commands
{
    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
    {
        void Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TOut> : ICommandHandler
    {
        TOut Handle(TCommand command);
    }
}