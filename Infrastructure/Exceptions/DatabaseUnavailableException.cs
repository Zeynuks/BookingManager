using Domain.Enums;
using Domain.Exceptions;

namespace Infrastructure.Exceptions
{
    public class DatabaseUnavailableException : AppException
    {
        public DatabaseUnavailableException( string message, Exception? inner = null )
            : base( ErrorCode.Unavailable, message )
        {
        }
    }
}