namespace Infrastructure.Cqrs
{
    public interface IGate
    {
        void RunCommand<TCommand>(TCommand command);

        TResult RunCommand<TCommand, TResult>(TCommand command);

        TResponse RunQuery<TQuery, TResponse>(TQuery query);

        TResponse RunQuery<TResponse>();
    }
}