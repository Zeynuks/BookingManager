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

        public async Task<RoomType?> TryGet( int id )
        {
            return await _dbContext.RoomTypes
                .Include( x => x.Services )
                .Include( x => x.Amenities )
                .AsSplitQuery()
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<IReadOnlyList<RoomType>> GetListByProperty( int propertyId )
        {
            return await _dbContext.RoomTypes
                .Where( x => x.PropertyId == propertyId )
                .Include( x => x.Services )
                .Include( x => x.Amenities )
                .AsSplitQuery()
                .AsNoTracking()
                .OrderBy( x => x.Name )
                .ToListAsync();
        }


        public void Add( RoomType roomType )
        {
            _dbContext.Add( roomType );
        }

        public void Delete( RoomType roomType )
        {
            _dbContext.RoomTypes.Remove( roomType );
        }

        public IQueryable<RoomType> Query()
        {
            return _dbContext.RoomTypes;
        }

        public Task<int> Count( IQueryable<RoomType> query )
        {
            return query.CountAsync();
        }

        public async Task<IReadOnlyList<RoomType>> GetPage(
            IQueryable<RoomType> query,
            int page,
            int size )
        {
            return await query
                .AsNoTracking()
                .Skip( ( page - 1 ) * size )
                .Take( size )
                .ToListAsync();
        }
    }
}