namespace Infrastructure.Cqrs
{
    public class StandardGate : IGate
    {
        private readonly IRunEnvironment _runEnvironment;

        public StandardGate(IRunEnvironment environment)
        {
            _runEnvironment = environment;
        }

        public void RunCommand<T>(T command)
        {
            _runEnvironment.RunCommand(command);
        }

        public TOut RunCommand<T, TOut>(T command)
        {
            return _runEnvironment.RunCommand<T, TOut>(command);
        }

        public TResponse RunQuery<TQuery, TResponse>(TQuery query)
        {
            return _runEnvironment.RunQuery<TQuery, TResponse>(query);
        }

        public TResponse RunQuery<TResponse>()
        {
            return _runEnvironment.RunQuery<TResponse>();
        }
    }
}