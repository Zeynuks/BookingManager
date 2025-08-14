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
            return await _dbContext.Amenities
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Amenity>> GetList( CancellationToken ct )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .ToListAsync( ct );
        }

        public async Task<List<Amenity>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken ct )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .Where( a => ids.Contains( a.Id ) )
                .ToListAsync( ct );
        }

        public async Task<List<Amenity>> GetListByRoomType( int roomTypeId, CancellationToken ct )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .Where( a => a.RoomTypes.Any( t => t.Id == roomTypeId ) )
                .ToListAsync( ct );
        }

        public void Add( Amenity amenity )
        {
            _dbContext.Amenities.Add( amenity );
        }

        public void Delete( Amenity amenity )
        {
            _dbContext.Amenities.Remove( amenity );
        }
    }
}