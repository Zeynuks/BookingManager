using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; private init; }
        public int RoomId { get; private set; }
        public int GuestId { get; private set; }
        public int GuestsCount { get; private set; }
        public DateOnly ArrivalDate { get; private set; }
        public DateOnly DepartureDate { get; private set; }
        public TimeOnly ArrivalTime { get; private set; }
        public TimeOnly DepartureTime { get; private set; }
        public decimal Total { get; private set; }
        public Currency Currency { get; private set; }

        public virtual  Room Room { get; private set; }
        public virtual  Guest Guest { get; private set; }

        public Reservation(
            int roomId,
            int guestId,
            int guestsCount,
            DateOnly arrivalDate,
            DateOnly departureDate,
            TimeOnly arrivalTime,
            TimeOnly departureTime,
            decimal total,
            Currency currency )
        {
            RoomId = roomId;
            GuestId = guestId;
            GuestsCount = guestsCount;
            ( ArrivalDate, DepartureDate ) = ValidateDates( arrivalDate, departureDate );
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Total = ValidateTotal( total );
            Currency = ValidateCurrency( currency );
        }

        public void Update(
            int guestsCount,
            DateOnly arrivalDate,
            DateOnly departureDate,
            TimeOnly arrivalTime,
            TimeOnly departureTime,
            decimal total,
            Currency currency )
        {
            GuestsCount = ValidateGuestsCount( guestsCount );
            ( ArrivalDate, DepartureDate ) = ValidateDates( arrivalDate, departureDate );
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Total = ValidateTotal( total );
            Currency = ValidateCurrency( currency );
        }

        private static int ValidateGuestsCount( int guestsCount )
        {
            if ( guestsCount <= 0 )
            {
                throw new DomainValidationException( "GuestsCount must be greater than zero." );
            }

            return guestsCount;
        }

        private static (DateOnly arrival, DateOnly departure) ValidateDates( DateOnly arrival, DateOnly departure )
        {
            if ( arrival >= departure )
            {
                throw new DomainValidationException( "DepartureDate must be after ArrivalDate." );
            }

            return ( arrival, departure );
        }

        private static decimal ValidateTotal( decimal total )
        {
            if ( total <= 0 )
            {
                throw new DomainValidationException( "Total price cannot be negative or null." );
            }

            return total;
        }

        private static Currency ValidateCurrency( Currency currency )
        {
            if ( !Enum.IsDefined( typeof( Currency ), currency ) )
            {
                throw new DomainValidationException( "Invalid currency value." );
            }

            return currency;
        }
    }
}