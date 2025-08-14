using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public GuestRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Guest?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Guest>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Guest>> GetList( CancellationToken ct )
        {
            return await _dbContext.Set<Guest>().ToListAsync( ct );
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