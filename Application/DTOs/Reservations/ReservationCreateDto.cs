namespace Application.DTOs.Reservations
{
    public class ReservationCreateDto : ReservationDto
    {
        public ReservationCreateDto(
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