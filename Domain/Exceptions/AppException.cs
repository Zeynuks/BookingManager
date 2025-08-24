using Domain.Enums;

namespace Domain.Exceptions
{
    public class AppException : Exception
    {
        protected AppException( ErrorCode code, string message ) : base( message )
        {
            Code = code;
        }

        public ErrorCode Code { get; }
    }
}