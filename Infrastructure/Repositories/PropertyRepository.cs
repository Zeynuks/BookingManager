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

        public async Task<Property?> TryGet( int id, CancellationToken cancellationToken )
        {
            return await _dbContext.Properties
                .FirstOrDefaultAsync( x => x.Id == id, cancellationToken );
        }

        public async Task<IReadOnlyList<Property>> GetList( CancellationToken cancellationToken )
        {
            return await _dbContext.Properties
                .AsNoTracking()
                .ToListAsync( cancellationToken );
        }

        public void Add( Property property )
        {
            _dbContext.Add( property );
        }

        public void Delete( Property property )
        {
            _dbContext.Properties.Remove( property );
        }
    }
}