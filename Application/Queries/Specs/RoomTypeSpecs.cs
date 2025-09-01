using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Queries.Specs
{
    public static class RoomTypeSpecs
    {
        public static Expression<Func<RoomType, bool>> HasAvailabilityOn(
            DateOnly arrivalDate,
            DateOnly departureDate,
            int requiredGuests )
            =>
                roomType =>
                    (
                        !roomType.IsSharedOccupancy
                        && requiredGuests <= roomType.MaxPlaces
                        && roomType.Rooms.Any(
                            room => room.Reservations.All(
                                reservation =>
                                    reservation.DepartureDate <= arrivalDate
                                    || reservation.ArrivalDate >= departureDate
                            )
                        )
                    )
                    ||
                    (
                        roomType.IsSharedOccupancy
                        && roomType.Rooms.Count() * roomType.MaxPlaces
                        - roomType.Rooms
                            .SelectMany( r => r.Reservations )
                            .Count( reservation =>
                                !( reservation.DepartureDate <= arrivalDate
                                   || reservation.ArrivalDate >= departureDate )
                            ) >= requiredGuests
                    );

        public static Expression<Func<RoomType, bool>> HasAvailabilityToday( int requiredGuests )
        {
            DateOnly today = DateOnly.FromDateTime( DateTime.UtcNow.Date );
            DateOnly tomorrow = today.AddDays( 1 );
            return HasAvailabilityOn( today, tomorrow, requiredGuests );
        }

        public static Expression<Func<RoomType, bool>> CapacityAtLeast( int guests )
            => roomType => roomType.MaxPlaces >= guests;

        public static Expression<Func<RoomType, bool>> DailyPriceBetween( decimal? minDailyPrice,
            decimal? maxDailyPrice )
            =>
                roomType =>
                    ( !minDailyPrice.HasValue || roomType.DailyPrice >= minDailyPrice.Value )
                    && ( !maxDailyPrice.HasValue || roomType.DailyPrice <= maxDailyPrice.Value );

        public static Expression<Func<RoomType, bool>> HasAllAmenities( int[] amenityIds )
            => roomType => amenityIds.All( amenityId => roomType.Amenities.Any( a => a.Id == amenityId ) );

        public static Expression<Func<RoomType, bool>> HasAllServices( int[] serviceIds )
            => roomType => serviceIds.All( serviceId => roomType.Services.Any( s => s.Id == serviceId ) );
    }
}