using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGuestRepository
    {
        Task<Guest?> Get(int id, CancellationToken ct);
        Task<List<Guest>> List(CancellationToken ct);
        void Add(Guest guest);
        void Delete(Guest guest);
    }
}