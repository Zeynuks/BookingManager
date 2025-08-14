namespace Application.DTOs.Reservations
{
    public class ReservationUpdateDto : ReservationDto
    {
        public ReservationUpdateDto(
            int roomId,
            int guestId,
            int guestsCount,
            DateOnly arrivalDate,
            DateOnly departureDate,
            TimeOnly arrivalTime,
            TimeOnly departureTime,
            string currency )
            : base( roomId, guestId, guestsCount, arrivalDate, departureDate, arrivalTime, departureTime, currency )
        {
        }
    }
}