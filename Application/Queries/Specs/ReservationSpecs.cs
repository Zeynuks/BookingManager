using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Queries.Specs
{
    public static class ReservationSpecs
    {
        public static Expression<Func<Reservation, bool>> ByGuest( int guestId )
        {
            return reservation => reservation.GuestId == guestId;
        }

        public static Expression<Func<Reservation, bool>> ByRoomType( int roomTypeId )
        {
            return reservation => reservation.Room.RoomTypeId == roomTypeId;
        }

        public static Expression<Func<Reservation, bool>> ByProperty( int propertyId )
        {
            return reservation => reservation.Room.RoomType.PropertyId == propertyId;
        }

        public static Expression<Func<Reservation, bool>> FromDateInclusive( DateOnly fromDate )
        {
            return reservation => reservation.DepartureDate >= fromDate;
        }

        public static Expression<Func<Reservation, bool>> ToDateInclusive( DateOnly toDate )
        {
            return reservation => reservation.ArrivalDate <= toDate;
        }
    }
}