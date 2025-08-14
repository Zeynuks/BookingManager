using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public AmenityRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Amenity?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Amenity>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Amenity>> List( CancellationToken ct )
        {
            return await _dbContext.Set<Amenity>().ToListAsync( ct );
        }

        public async Task<List<Amenity>> ListByIds( IReadOnlyCollection<int> ids, CancellationToken ct )
        {
            return await _dbContext.Set<Amenity>()
                .Where( a => ids.Contains( a.Id ) )
                .ToListAsync( ct );
        }

        public async Task<List<Amenity>> ListByRoomType( int roomTypeId, CancellationToken ct )
        {
            return await _dbContext.Set<Amenity>()
                .Where( s => s.RoomTypes.Any( t => t.Id == roomTypeId ) )
                .ToListAsync( ct );
        }

        public void Add( Amenity amenity )
        {
            _dbContext.Add( amenity );
        }

        public void Delete( Amenity amenity )
        {
            _dbContext.Set<Amenity>().Remove( amenity );
        }
    }
}