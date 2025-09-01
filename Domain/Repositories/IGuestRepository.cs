using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGuestRepository
    {
        Task<Guest?> TryGet( int id );
        Task<IReadOnlyList<Guest>> GetReadOnlyList();
        void Add( Guest guest );
        void Delete( Guest guest );
    }
}