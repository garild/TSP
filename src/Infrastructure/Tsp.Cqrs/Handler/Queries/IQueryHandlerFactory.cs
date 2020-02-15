namespace Tsp.Cqrs.Handler.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResponse> CreateQuery<TQuery, TResponse>();

        IQueryHandler<TResponse> CreateQuery<TResponse>();
    }
}
