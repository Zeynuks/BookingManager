using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly BookingManagerDbContext _dbContext;

        public ServiceRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Service?> Get( int id )
        {
            return await _dbContext.Set<Service>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<Service>> List()
        {
            return await _dbContext.Set<Service>().ToListAsync();
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