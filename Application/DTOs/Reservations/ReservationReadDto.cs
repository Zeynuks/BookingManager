using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Reservations
{
    public class ReservationReadDto : ReservationDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [Range( 0, double.MaxValue )]
        public decimal Total { get; init; }

        public ReservationReadDto(
            int id,
            int roomId,
            int guestId,
            int guestsCount,
            DateOnly arrivalDate,
            DateOnly departureDate,
            TimeOnly arrivalTime,
            TimeOnly departureTime,
            decimal total,
            string currency )
            : base( roomId, guestId, guestsCount, arrivalDate, departureDate, arrivalTime, departureTime, currency )
        {
            Id = id;
            Total = total;
        }
    }
}