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

        public async Task<Amenity?> TryGet( int id, CancellationToken cancellationToken )
        {
            return await _dbContext.Amenities
                .FirstOrDefaultAsync( x => x.Id == id, cancellationToken );
        }

        public async Task<IReadOnlyList<Amenity>> GetList( CancellationToken cancellationToken )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .ToListAsync( cancellationToken );
        }

        public async Task<IReadOnlyList<Amenity>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken cancellationToken )
        {
            return await _dbContext.Amenities
                .AsNoTracking()
                .Where( a => ids.Contains( a.Id ) )
                .ToListAsync( cancellationToken );
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