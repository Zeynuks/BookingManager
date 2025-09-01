using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Reservations
{
    public class ReservationDto
    {
        [Required]
        public int RoomId { get; init; }

        [Required]
        public int GuestId { get; init; }

        [Required]
        [Range( 0, int.MaxValue )]
        public int GuestsCount { get; init; }

        [Required]
        public DateOnly ArrivalDate { get; init; }

        [Required]
        public DateOnly DepartureDate { get; init; }

        [Required]
        [DefaultValue( "08:30:00" )]
        public TimeOnly ArrivalTime { get; init; }

        [Required]
        [DefaultValue( "08:30:00" )]
        public TimeOnly DepartureTime { get; init; }

        [Required]
        [DefaultValue( "RUB" )]
        [StringLength( 3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter ISO code." )]
        public string Currency { get; init; }

        protected ReservationDto(
            int roomId,
            int guestId,
            int guestsCount,
            DateOnly arrivalDate,
            DateOnly departureDate,
            TimeOnly arrivalTime,
            TimeOnly departureTime,
            string currency )
        {
            RoomId = roomId;
            GuestId = guestId;
            GuestsCount = guestsCount;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Currency = currency;
        }
    }
}