using System;
using Tsp.ExceptionHandling.Exceptions.Abstractions;

namespace Tsp.ExceptionHandling.Exceptions
{
    public class BadRequestException : Exception, IDomainException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }

        public int HttpStatusCode => 400;

        public string Code => "BadRequest";
    }
}
