using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomTypeDto
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; init; }

        [Required]
        [Range( 0, double.MaxValue )]
        public decimal DailyPrice { get; init; }

        [Required]
        [Range( 1, int.MaxValue )]
        public int MaxPlaces { get; init; }

        [Required]
        public bool IsSharedOccupancy { get; init; }

        protected RoomTypeDto(
            string name,
            decimal dailyPrice,
            int maxPlaces,
            bool isSharedOccupancy )
        {
            Name = name;
            DailyPrice = dailyPrice;
            MaxPlaces = maxPlaces;
            IsSharedOccupancy = isSharedOccupancy;
        }
    }
}