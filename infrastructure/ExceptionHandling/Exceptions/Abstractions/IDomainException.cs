namespace ExceptionHandling.Exceptions.Abstractions
{
    public interface IDomainException
    {
        int HttpStatusCode { get; }
        string Code { get; }
        string Message { get; }
    }
}
