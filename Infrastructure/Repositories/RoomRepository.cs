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

        public async Task<Room?> TryGet( int id )
        {
            return await _dbContext.Rooms
                .Include( r => r.RoomType )
                .FirstOrDefaultAsync( x => x.Id == id );
        }

        public async Task<IReadOnlyList<Room>> GetListByRoomType( int roomTypeId )
        {
            return await _dbContext.Rooms
                .Where( x => x.RoomTypeId == roomTypeId )
                .OrderBy( x => x.Number )
                .AsNoTracking()
                .ToListAsync();
        }

        public void Add( Room room )
        {
            _dbContext.Add( room );
        }

        public void Delete( Room room )
        {
            _dbContext.Rooms.Remove( room );
        }

        public IQueryable<Room> Query()
        {
            return _dbContext.Rooms;
        }

        public async Task<bool> IsAvailable(
            int roomId,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int guestsCount )
        {
            Room room = await _dbContext.Rooms
                .AsNoTracking()
                .Include( r => r.RoomType )
                .SingleAsync( r => r.Id == roomId );

            IQueryable<Reservation> overlapQuery = _dbContext.Reservations
                .Where( x => x.RoomId == roomId )
                .Where( x =>
                    ( x.ArrivalDate < departureDate ||
                      ( x.ArrivalDate == departureDate && x.ArrivalTime < departureTime ) )
                    &&
                    ( arrivalDate < x.DepartureDate ||
                      ( arrivalDate == x.DepartureDate && arrivalTime < x.DepartureTime ) )
                );

            if ( !room.RoomType.IsSharedOccupancy )
            {
                bool hasOverlap = await overlapQuery.AnyAsync();

                return !hasOverlap;
            }

            int? occupiedNullable = await overlapQuery
                .Select( x => x.GuestsCount )
                .SumAsync();

            int occupied = occupiedNullable ?? 0;

            return occupied + guestsCount <= room.RoomType.MaxPlaces;
        }
    }
}