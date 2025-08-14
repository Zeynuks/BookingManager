using Infrastructure.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation
{
    public class UnitOfWorkExceptionDecorator : IUnitOfWork
    {
        private readonly IUnitOfWork _inner;

        public UnitOfWorkExceptionDecorator( IUnitOfWork inner )
        {
            _inner = inner;
        }

        public async Task CommitAsync( CancellationToken ct )
        {
            try
            {
                await _inner.CommitAsync( ct );
            }
            catch ( DbUpdateConcurrencyException ex )
            {
                throw new ConcurrencyConflictException( "Resource was modified by another process.", ex );
            }
            catch ( DbUpdateException ex ) when ( IsUniqueConstraintViolation( ex ) )
            {
                throw new UniqueConstraintViolationException( "Duplicate value violates unique constraint.", ex );
            }
            catch ( DbUpdateException ex ) when ( IsDatabaseUnavailable( ex ) )
            {
                throw new DatabaseUnavailableException( "Database is unavailable.", ex );
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected infrastructure error occurred.", ex);
            }
            
        }

        private static bool IsUniqueConstraintViolation( DbUpdateException ex )
        {
            return ex.InnerException is SqlException { Number: 2627 or 2601 };
        }

        private static bool IsDatabaseUnavailable( DbUpdateException ex )
        {
            return ex.InnerException is SqlException { Number: 4060 or 18456 or -2 or 53 };
        }
    }
}