using Domain.Entities;

namespace Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> TryGet( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<Service>> GetList( CancellationToken cancellationToken );
        Task<IReadOnlyList<Service>> GetListByIds( IReadOnlyCollection<int> ids, CancellationToken cancellationToken );
        void Add( Service service );
        void Delete( Service service );
    }
}