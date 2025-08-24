using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGuestRepository
    {
        Task<Guest?> TryGet( int id, CancellationToken cancellationToken );
        Task<IReadOnlyList<Guest>> GetList( CancellationToken cancellationToken );
        void Add( Guest guest );
        void Delete( Guest guest );
    }
}