using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAmenityRepository
    {
        Task<Amenity?> Get( int id, CancellationToken ct );
        Task<List<Amenity>> GetList( CancellationToken ct );
        Task<List<Amenity>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken ct );
        Task<List<Amenity>> GetListByRoomType( int roomTypeId, CancellationToken ct );
        void Add( Amenity amenity );
        void Delete( Amenity amenity );
    }
}