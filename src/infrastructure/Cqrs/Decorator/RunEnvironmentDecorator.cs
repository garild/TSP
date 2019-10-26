namespace Infrastructure.Cqrs.Decorator
{
    internal class RunEnvironmentDecorator : IRunEnvironment
    {
        private readonly IRunEnvironment _runEnvironment;

        public RunEnvironmentDecorator(IRunEnvironment runEnvironment)
        {
            _runEnvironment = runEnvironment;
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