using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AmenityRepository : IRepository<Amenity>
    {
        private readonly BookingManagerDbContext _dbContext;

        public AmenityRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Amenity?> Get( int id )
        {
            return await _dbContext.Set<Amenity>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<Amenity>> List()
        {
            return await _dbContext.Set<Amenity>().ToListAsync();
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