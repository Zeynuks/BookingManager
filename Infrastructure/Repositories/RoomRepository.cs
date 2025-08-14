using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public RoomRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<Room?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<Room>().FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<Room>> ListByRoomType( int roomTypeId, CancellationToken ct )
        {
            return await _dbContext.Set<Room>()
                .Where( x => x.RoomTypeId == roomTypeId )
                .OrderBy( x => x.Number )
                .ToListAsync( ct );
        }

        public void Add( Room room )
        {
            _dbContext.Add( room );
        }

        public void Delete( Room room )
        {
            _dbContext.Set<Room>().Remove( room );
        }
        
        public IQueryable<Room> Query()
        {
            return _dbContext.Set<Room>();
        }
    }
}