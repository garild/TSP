using System;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Tsp.Cqrs.Decorator
{
    internal class RequestLoggerDecorator : IRunEnvironment
    {
        private readonly ILogger _logger;
        private readonly IRunEnvironment _runEnvironment;

        public RequestLoggerDecorator(IRunEnvironment runEnvironment,
            ILoggerFactory logger)
        {
            _runEnvironment = runEnvironment;
            _logger = logger.CreateLogger<RequestLoggerDecorator>();
        }

        public TOut RunCommand<T, TOut>(T command, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    $"{typeof(T).FullName} serialized input command: {JsonSerializer.Serialize(command)}");
                return _runEnvironment.RunCommand<T, TOut>(command, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"An error occurred while executing {typeof(T).FullName}, serialized input command: {JsonSerializer.Serialize(command)}");
                throw;
            }
        }

        public void RunCommand<T>(T command, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    $"{typeof(T).FullName} serialized input command: {JsonSerializer.Serialize(command)}");
                _runEnvironment.RunCommand(command, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"An error occurred while executing {typeof(T).FullName}, serialized input command: {JsonSerializer.Serialize(command)}");
                throw;
            }
        }

        public TResponse RunQuery<TQuery, TResponse>(TQuery query)
        {
            try
            {
                _logger.LogInformation(
                      $"{typeof(TQuery).FullName} serialized query input: {JsonSerializer.Serialize(query)}");

                return _runEnvironment.RunQuery<TQuery, TResponse>(query);
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"An error occurred while executing {typeof(TQuery).FullName}, serialized input command: {JsonSerializer.Serialize(query)}");
                throw;
            }
        }

        public TResponse RunQuery<TResponse>()
        {
            try
            {
                _logger.LogInformation(
                      $"Start handle for query {typeof(TResponse).FullName}");

                return _runEnvironment.RunQuery<TResponse>();
            }
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"An error occurred while executing {typeof(TResponse).FullName} , error message : {e.Message}");
                throw;
            }
        }
    }
}