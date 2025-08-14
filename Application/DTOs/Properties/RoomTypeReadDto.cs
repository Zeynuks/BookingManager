using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
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
}