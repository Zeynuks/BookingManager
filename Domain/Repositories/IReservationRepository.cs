using Domain.Entities;

namespace Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation?> Get(int id, CancellationToken ct);
        Task<List<Reservation>> GetList(CancellationToken ct);
        void Add(Reservation reservation);
        void Delete(Reservation reservation);
        IQueryable<Reservation> Query();
    }
}