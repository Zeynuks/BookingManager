namespace Infrastructure.Foundation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingManagerDbContext _dbContext;

        public UnitOfWork( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync( CancellationToken ct )
        {
            await _dbContext.SaveChangesAsync( ct );
        }
    }
}