using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PropertyRepository : IRepository<Property>
    {
        private readonly BookingManagerDbContext _dbContext;

        public PropertyRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Property?> Get( int id )
        {
            return await _dbContext.Set<Property>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<Property>> List()
        {
            return await _dbContext.Set<Property>().ToListAsync();
        }

        public void Add( Property property )
        {
            _dbContext.Add( property );
        }

        public void Delete( Property property )
        {
            _dbContext.Set<Property>().Remove( property );
        }
    }
}