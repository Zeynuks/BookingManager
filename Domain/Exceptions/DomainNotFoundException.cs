using Domain.Enums;

namespace Domain.Exceptions
{
    public class DomainNotFoundException : AppException
    {
        public DomainNotFoundException( string message ) : base( ErrorCode.NotFound, message ) { }
    }
}