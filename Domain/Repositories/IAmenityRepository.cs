using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAmenityRepository
    {
        Task<Amenity?> TryGet( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<Amenity>> GetList( CancellationToken cancellationToken );
        Task<IReadOnlyList<Amenity>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken cancellationToken );
        void Add( Amenity amenity );
        void Delete( Amenity amenity );
    }
}