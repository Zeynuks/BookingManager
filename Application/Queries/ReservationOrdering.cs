using Application.DTOs.Reservations;
using Domain.Entities;

namespace Application.Queries
{
    public static class ReservationOrdering
    {
        public static IOrderedQueryable<Reservation> OrderForSearch(
            IQueryable<Reservation> source,
            ReservationSortBy sortBy,
            bool descending )
        {
            return ( sortBy, descending ) switch
            {
                (ReservationSortBy.RoomType, false) => source.OrderBy( reservation => reservation.Room.RoomTypeId )
                    .ThenBy( reservation => reservation.Id ),
                (ReservationSortBy.RoomType, true) => source
                    .OrderByDescending( reservation => reservation.Room.RoomTypeId )
                    .ThenByDescending( reservation => reservation.Id ),

                (ReservationSortBy.ArrivalDate, false) => source.OrderBy( reservation => reservation.ArrivalDate )
                    .ThenBy( reservation => reservation.Id ),
                (ReservationSortBy.ArrivalDate, true) => source
                    .OrderByDescending( reservation => reservation.ArrivalDate )
                    .ThenByDescending( reservation => reservation.Id ),

                (ReservationSortBy.DepartureDate, false) => source.OrderBy( reservation => reservation.DepartureDate )
                    .ThenBy( reservation => reservation.Id ),
                (ReservationSortBy.DepartureDate, true) => source
                    .OrderByDescending( reservation => reservation.DepartureDate )
                    .ThenByDescending( reservation => reservation.Id ),

                _ => source.OrderBy( reservation => reservation.Id )
            };
        }
    }
}