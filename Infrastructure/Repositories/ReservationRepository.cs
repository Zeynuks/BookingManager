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

        public async Task<Reservation?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Reservation>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Reservation>> GetList( CancellationToken ct )
        {
            return await _dbContext.Set<Reservation>().ToListAsync( ct );
        }

        public void Add( Reservation reservation )
        {
            _dbContext.Add( reservation );
        }

        public void Delete( Reservation reservation )
        {
            _dbContext.Set<Reservation>().Remove( reservation );
        }
        
        public IQueryable<Reservation> Query()
        {
            return _dbContext.Set<Reservation>();
        }
    }
}