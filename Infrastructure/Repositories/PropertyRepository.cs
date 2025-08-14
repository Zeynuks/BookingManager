using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public PropertyRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Property?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Property>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Property>> List( CancellationToken ct )
        {
            return await _dbContext.Set<Property>().ToListAsync( ct );
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