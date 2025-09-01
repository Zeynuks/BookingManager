using Application.DTOs.Reservations;
using Domain.Entities;

namespace Application.Queries.Interfaces
{
    public interface IReservationQueryBuilder
    {
        IQueryable<Reservation> Build( ReservationSearchQueryDto query );
    }
}