using Domain.Entities;

namespace Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> Get( int id, CancellationToken ct );
        Task<List<Service>> List( CancellationToken ct );
        Task<List<Service>> ListByIds( IReadOnlyCollection<int> ids, CancellationToken ct );
        Task<List<Service>> ListByRoomType( int roomTypeId, CancellationToken ct );
        void Add( Service service );
        void Delete( Service service );
    }
}