using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<RoomType?> TryGet( int id );
        Task<IReadOnlyList<RoomType>> GetListByProperty( int propertyId );
        void Add( RoomType roomType );
        void Delete( RoomType roomType );
        IQueryable<RoomType> Query();

        public Task<int> Count( IQueryable<RoomType> query );

        Task<IReadOnlyList<RoomType>> GetPage( IQueryable<RoomType> query, int page, int size );
    }
}