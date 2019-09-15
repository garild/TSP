namespace Infrastructure.Cqrs.Handler.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> CreateCommand<T>();

        ICommandHandler<TO, T> CreateCommand<TO, T>();
    }
}