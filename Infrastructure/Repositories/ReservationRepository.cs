using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public ReservationRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Reservation?> TryGet( int id )
        {
            return await _dbContext.Reservations
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public void Add( Reservation reservation )
        {
            _dbContext.Add( reservation );
        }

        public void Delete( Reservation reservation )
        {
            _dbContext.Reservations.Remove( reservation );
        }

        public IQueryable<Reservation> Query()
        {
            return _dbContext.Reservations;
        }

        public Task<int> Count( IQueryable<Reservation> query )
        {
            return query.CountAsync();
        }

        public async Task<IReadOnlyList<Reservation>> GetPage( IQueryable<Reservation> query, int page, int size )
        {
            return await query
                .AsNoTracking()
                .Skip( ( page - 1 ) * size )
                .Take( size )
                .ToListAsync();
        }
    }
}