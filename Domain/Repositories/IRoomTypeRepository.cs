using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> Get( int id, CancellationToken ct );
        Task<List<RoomType>> GetListByProperty( int propertyId, CancellationToken ct );
        void Add( RoomType roomType );
        void Delete( RoomType roomType );
        IQueryable<RoomType> Query();
    }
}