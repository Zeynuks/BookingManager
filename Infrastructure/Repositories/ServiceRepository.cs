using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public ServiceRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Service?> TryGet( int id, CancellationToken cancellationToken )
        {
            return await _dbContext.Services
                .FirstOrDefaultAsync( x => x.Id == id, cancellationToken );
        }

        public async Task<IReadOnlyList<Service>> GetList( CancellationToken cancellationToken )
        {
            return await _dbContext.Services
                .AsNoTracking()
                .ToListAsync( cancellationToken );
        }

        public async Task<IReadOnlyList<Service>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken cancellationToken )
        {
            return await _dbContext.Services
                .Where( s => ids.Contains( s.Id ) )
                .AsNoTracking()
                .ToListAsync( cancellationToken );
        }

        public void Add( Service service )
        {
            _dbContext.Add( service );
        }

        public void Delete( Service service )
        {
            _dbContext.Services.Remove( service );
        }
    }
}