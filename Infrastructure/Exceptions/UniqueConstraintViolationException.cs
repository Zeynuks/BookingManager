using Domain.Enums;
using Domain.Exceptions;

namespace Infrastructure.Exceptions
{
    public class UniqueConstraintViolationException : AppException
    {
        public UniqueConstraintViolationException( string message, Exception? inner = null )
            : base( ErrorCode.Conflict, message )
        {
        }
    }
}