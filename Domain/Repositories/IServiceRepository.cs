using Domain.Entities;

namespace Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> TryGet( int id );
        Task<IReadOnlyList<Service>> GetReadOnlyList();
        Task<IReadOnlyList<Service>> GetReadOnlyListByIds( IReadOnlyCollection<int> ids );
        void Add( Service service );
        void Delete( Service service );
    }
}