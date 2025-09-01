using System.Collections.Generic;

namespace Application.DTOs.Properties
{
    public class RoomTypeUpdateDto
    {
        public string Name { get; init; }
        public decimal DailyPrice { get; init; }
        public int MaxPersonCount { get; init; }
        public bool IsSharedOccupancy { get; init; }
        public IReadOnlyCollection<int>? ServiceIds { get; init; }
        public IReadOnlyCollection<int>? AmenityIds { get; init; }

        public RoomTypeUpdateDto(
            string name,
            decimal dailyPrice,
            int maxPersonCount,
            bool isSharedOccupancy,
            IReadOnlyCollection<int>? serviceIds,
            IReadOnlyCollection<int>? amenityIds )
        {
            Name = name;
            DailyPrice = dailyPrice;
            MaxPersonCount = maxPersonCount;
            IsSharedOccupancy = isSharedOccupancy;
            ServiceIds = serviceIds;
            AmenityIds = amenityIds;
        }
    }
}