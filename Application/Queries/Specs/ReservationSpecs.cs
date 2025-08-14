using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Queries.Specs
{
    public static class ReservationSpecs
    {
        public static Expression<Func<Reservation, bool>> ByGuest( int guestId )
            => reservation => reservation.GuestId == guestId;

        public static Expression<Func<Reservation, bool>> ByRoomType( int roomTypeId )
            => reservation => reservation.Room.RoomTypeId == roomTypeId;

        public static Expression<Func<Reservation, bool>> ByProperty( int propertyId )
            => reservation => reservation.Room.RoomType.PropertyId == propertyId;

        public static Expression<Func<Reservation, bool>> FromDateInclusive( DateOnly fromDate )
            => reservation => reservation.DepartureDate >= fromDate;

        public static Expression<Func<Reservation, bool>> ToDateInclusive( DateOnly toDate )
            => reservation => reservation.ArrivalDate <= toDate;
    }
}