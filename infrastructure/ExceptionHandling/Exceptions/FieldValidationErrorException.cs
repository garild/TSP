using ExceptionHandling.Exceptions.Abstractions;
using System;

namespace ExceptionHandling.Exceptions
{
    public class FieldValidationErrorException : Exception, IDomainException
    {
        public FieldValidationErrorException(string message)
            : base(message)
        {
        }

        public int HttpStatusCode => 400;

        public string Code => "ValidationException";
    }
}
