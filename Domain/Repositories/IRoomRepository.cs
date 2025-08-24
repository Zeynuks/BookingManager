using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> TryGet( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<Room>> GetListByRoomType( int roomTypeId, CancellationToken cancellationToken );
        void Add( Room room );
        void Delete( Room room );
        public IQueryable<Room> Query();

        Task<bool> IsAvailable(
            int roomId,
            DateOnly arrivalDate,
            TimeOnly arrivalTime,
            DateOnly departureDate,
            TimeOnly departureTime,
            int guestsCount,
            CancellationToken cancellationToken );
    }
}