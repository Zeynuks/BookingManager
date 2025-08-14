using Application.DTOs.Reservations;
using Domain.Entities;

namespace Application.Queries.Interfaces
{
    public interface IReservationQueryBuilder
    {
        IQueryable<Reservation> Build( IQueryable<Reservation> source, ReservationSearchQueryDto query );
    }
}