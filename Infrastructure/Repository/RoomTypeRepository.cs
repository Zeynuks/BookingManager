using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private readonly BookingManagerDbContext _dbContext;

        public RoomTypeRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<RoomType?> Get( int id )
        {
            return await _dbContext.Set<RoomType>().FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<List<RoomType>> List()
        {
            return await _dbContext.Set<RoomType>().ToListAsync();
        }

        public void Add( RoomType roomType )
        {
            _dbContext.Add( roomType );
        }

        public void Delete( RoomType roomType )
        {
            _dbContext.Set<RoomType>().Remove( roomType );
        }
    }
}