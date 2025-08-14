using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAmenityRepository
    {
        Task<Amenity?> Get( int id, CancellationToken ct );
        Task<List<Amenity>> List( CancellationToken ct );
        Task<List<Amenity>> ListByIds( IReadOnlyCollection<int> ids, CancellationToken ct );
        Task<List<Amenity>> ListByRoomType( int roomTypeId, CancellationToken ct );
        void Add( Amenity amenity );
        void Delete( Amenity amenity );
    }
}