using Domain.Enums;

namespace Domain.Exceptions
{
    public class DomainValidationException : AppException
    {
        public DomainValidationException( string message ) : base( ErrorCode.Validation, message ) { }
    }
}