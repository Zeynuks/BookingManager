using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomTypeSearchQueryDto
    {
        public string? City { get; init; }
        public decimal? PriceMin { get; init; }
        public decimal? PriceMax { get; init; }
        public int? Guests { get; init; }
        public int[]? AmenityIds { get; init; }
        public int[]? ServiceIds { get; init; }
        public DateOnly? ArrivalDate { get; init; }
        public DateOnly? DepartureDate { get; init; }
        public RoomTypeSortBy SortBy { get; init; } = RoomTypeSortBy.Price;
        public bool Desc { get; init; }

        [Range( 1, int.MaxValue )]
        public int Page { get; init; } = 1;

        [Range( 1, 200 )]
        public int Size { get; init; } = 20;
    }
}