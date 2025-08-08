using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly BookingManagerDbContext _dbContext;

        public ReservationRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Reservation?> Get( int id )
        {
            return await _dbContext.Set<Reservation>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<Reservation>> List()
        {
            return await _dbContext.Set<Reservation>().ToListAsync();
        }

        public void Add( Reservation reservation )
        {
            _dbContext.Add( reservation );
        }

        public void Delete( Reservation reservation )
        {
            _dbContext.Set<Reservation>().Remove( reservation );
        }
    }
}