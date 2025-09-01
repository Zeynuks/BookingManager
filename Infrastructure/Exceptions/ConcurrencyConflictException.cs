using Domain.Enums;
using Domain.Exceptions;

namespace Infrastructure.Exceptions
{
    public class ConcurrencyConflictException : AppException
    {
        public ConcurrencyConflictException( string message, Exception? inner = null )
            : base( ErrorCode.Conflict, message )
        {
        }
    }
}