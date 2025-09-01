using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> TryGet( int id );
        Task<IReadOnlyList<Room>> GetListByRoomType( int roomTypeId );
        void Add( Room room );
        void Delete( Room room );

        Task<bool> IsAvailable(
            int roomId,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int guestsCount );
    }
}