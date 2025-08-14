using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Reservations
{
    public class ReservationSearchQueryDto
    {
        public int? PropertyId { get; init; }
        public int? RoomTypeId { get; init; }
        public int? GuestId { get; init; }
        public DateOnly? From { get; init; }
        public DateOnly? To { get; init; }

        [Required]
        public ReservationSortBy SortBy { get; init; } = ReservationSortBy.ArrivalDate;

        public bool Desc { get; init; } = false;

        public int Page { get; init; } = 1;
        public int Size { get; init; } = 20;
    }
}