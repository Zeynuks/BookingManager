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

    public class RoomTypeReadDto : RoomTypeDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public int PropertyId { get; init; }

        [Required]
        public IReadOnlyCollection<ServiceReadDto> Services { get; init; }

        [Required]
        public IReadOnlyCollection<AmenityReadDto> Amenities { get; init; }

        public RoomTypeReadDto(
            int id,
            int propertyId,
            string name,
            decimal dailyPrice,
            int maxPlaces,
            bool isSharedOccupancy,
            IReadOnlyCollection<ServiceReadDto> services,
            IReadOnlyCollection<AmenityReadDto> amenities )
            : base( name, dailyPrice, maxPlaces, isSharedOccupancy )
        {
            Id = id;
            PropertyId = propertyId;
            Services = services;
            Amenities = amenities;
        }
    }

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