using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomTypeCreateDto : RoomTypeDto
    {
        [Required]
        public IReadOnlyCollection<int>? ServiceIds { get; init; }

        [Required]
        public IReadOnlyCollection<int>? AmenityIds { get; init; }

        public RoomTypeCreateDto(
            string name,
            decimal dailyPrice,
            int maxPersonCount,
            bool isSharedOccupancy,
            IReadOnlyCollection<int>? serviceIds,
            IReadOnlyCollection<int>? amenityIds )
            : base( name, dailyPrice, maxPersonCount, isSharedOccupancy )
        {
            ServiceIds = serviceIds;
            AmenityIds = amenityIds;
        }
    }
}