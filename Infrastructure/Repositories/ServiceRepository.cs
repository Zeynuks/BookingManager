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

        public async Task<Service?> TryGet( int id )
        {
            return await _dbContext.Services
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<IReadOnlyList<Service>> GetReadOnlyList()
        {
            return await _dbContext.Services
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Service>> GetReadOnlyListByIds( IReadOnlyCollection<int> ids )
        {
            return await _dbContext.Services
                .Where( s => ids.Contains( s.Id ) )
                .AsNoTracking()
                .ToListAsync();
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