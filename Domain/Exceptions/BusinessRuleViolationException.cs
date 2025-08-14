using Domain.Enums;

namespace Domain.Exceptions
{
    public class BusinessRuleViolationException : AppException
    {
        public BusinessRuleViolationException( string message ) : base( ErrorCode.Conflict, message ) { }
    }
}