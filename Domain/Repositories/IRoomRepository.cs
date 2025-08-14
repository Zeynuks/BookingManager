using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> Get( int id, CancellationToken ct );
        Task<List<Room>> ListByRoomType( int roomTypeId, CancellationToken ct );
        void Add( Room room );
        void Delete( Room room );
        public IQueryable<Room> Query();
    }
}