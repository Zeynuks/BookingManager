using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAmenityRepository
    {
        Task<Amenity?> TryGet( int id );
        Task<IReadOnlyList<Amenity>> GetReadOnlyList();
        Task<IReadOnlyList<Amenity>> GetReadOnlyListByIds( IReadOnlyCollection<int> ids );
        void Add( Amenity amenity );
        void Delete( Amenity amenity );
    }
}