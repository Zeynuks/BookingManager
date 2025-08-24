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

        public async Task<Guest?> TryGet( int id, CancellationToken cancellationToken )
        {
            return await _dbContext.Guests
                .FirstOrDefaultAsync( x => x.Id == id, cancellationToken );
        }

        public async Task<IReadOnlyList<Guest>> GetList( CancellationToken cancellationToken )
        {
            return await _dbContext.Guests
                .AsNoTracking()
                .ToListAsync( cancellationToken );
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