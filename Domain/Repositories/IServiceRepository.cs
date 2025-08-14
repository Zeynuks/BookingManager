using Domain.Entities;

namespace Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> Get( int id, CancellationToken ct );
        Task<List<Service>> GetList( CancellationToken ct );
        Task<List<Service>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken ct );
        Task<List<Service>> GetListByRoomType( int roomTypeId, CancellationToken ct );
        void Add( Service service );
        void Delete( Service service );
    }
}