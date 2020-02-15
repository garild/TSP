namespace Tsp.Cqrs
{
    public interface IRunEnvironment
    {
        void RunCommand<T>(T command);
        TOut RunCommand<T, TOut>(T command);

        TResponse RunQuery<TQuery, TResponse>(TQuery query);

        TResponse RunQuery<TResponse>();
    }
}