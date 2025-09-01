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

        public async Task<Amenity?> TryGet( int id )
        {
            return await _dbContext.Amenities
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<IReadOnlyList<Amenity>> GetReadOnlyList()
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Amenity>> GetReadOnlyListByIds( IReadOnlyCollection<int> ids )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .Where( a => ids.Contains( a.Id ) )
                .ToListAsync();
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