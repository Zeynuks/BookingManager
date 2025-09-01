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

        public async Task<Guest?> TryGet( int id )
        {
            return await _dbContext.Guests
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<IReadOnlyList<Guest>> GetReadOnlyList()
        {
            return await _dbContext.Guests
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Guest guest )
        {
            _dbContext.Add( guest );
        }

        public void Delete( Guest guest )
        {
            _dbContext.Guests.Remove( guest );
        }
    }
}