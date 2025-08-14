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

        public async Task<Service?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Service>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Service>> List( CancellationToken ct )
        {
            return await _dbContext.Set<Service>().ToListAsync( ct );
        }

        public async Task<List<Service>> ListByIds( IReadOnlyCollection<int> ids, CancellationToken ct )
        {
            return await _dbContext.Set<Service>()
                .Where( s => ids.Contains( s.Id ) )
                .ToListAsync( ct );
        }

        public async Task<List<Service>> ListByRoomType( int roomTypeId, CancellationToken ct )
        {
            return await _dbContext.Set<Service>()
                .Where( s => s.RoomTypes.Any( t => t.Id == roomTypeId ) )
                .ToListAsync( ct );
        }

        public void Add( Service service )
        {
            _dbContext.Add( service );
        }

        public void Delete( Service service )
        {
            _dbContext.Set<Service>().Remove( service );
        }
    }
}