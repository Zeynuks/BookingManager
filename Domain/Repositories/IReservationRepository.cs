using Domain.Entities;

namespace Domain.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation?> TryGet( int id );
        void Add( Reservation reservation );
        void Delete( Reservation reservation );
        IQueryable<Reservation> Query();

        public Task<int> Count( IQueryable<Reservation> query );

        Task<IReadOnlyList<Reservation>> GetPage( IQueryable<Reservation> query, int page, int size );
    }
}