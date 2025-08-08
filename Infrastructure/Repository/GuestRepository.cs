using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GuestRepository : IRepository<Guest>
    {
        private readonly BookingManagerDbContext _dbContext;

        public GuestRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Guest?> Get( int id )
        {
            return await _dbContext.Set<Guest>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<Guest>> List()
        {
            return await _dbContext.Set<Guest>().ToListAsync();
        }

        public void Add( Guest guest )
        {
            _dbContext.Add( guest );
        }

        public void Delete( Guest guest )
        {
            _dbContext.Set<Guest>().Remove( guest );
        }
    }
}