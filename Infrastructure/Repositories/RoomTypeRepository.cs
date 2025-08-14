using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly BookingManagerDbContext _dbContext;

        public RoomTypeRepository( BookingManagerDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task<RoomType?> Get( int id, CancellationToken ct )
        {
            return await _dbContext.Set<RoomType>()
                .Include( x => x.Services )
                .Include( x => x.Amenities )
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.Id == id, ct );
        }

        public async Task<List<RoomType>> GetListByProperty( int propertyId, CancellationToken ct )
        {
            return await _dbContext.Set<RoomType>()
                .Where( x => x.PropertyId == propertyId )
                .Include( x => x.Services )
                .Include( x => x.Amenities )
                .AsSplitQuery()
                .AsNoTracking()
                .OrderBy( x => x.Name )
                .ToListAsync( ct );
        }


        public void Add( RoomType roomType )
        {
            _dbContext.Add( roomType );
        }

        public void Delete( RoomType roomType )
        {
            _dbContext.Set<RoomType>().Remove( roomType );
        }
        
        public IQueryable<RoomType> Query()
        {
            return _dbContext.Set<RoomType>();
        }
    }
}