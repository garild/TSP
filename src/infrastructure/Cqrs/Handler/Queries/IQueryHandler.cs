namespace Cqrs.Handler.Queries
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<out TResponse> : IQueryHandler
    {
        TResponse Handle();
    }

    public interface IQueryHandler<in TQuery, out TResponse> : IQueryHandler
    {
        TResponse Handle(TQuery request);
    }
}
