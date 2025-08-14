using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomReadDto : RoomDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public int RoomTypeId { get; init; }

        public RoomReadDto(
            int id,
            int roomTypeId,
            string number )
            : base( number )
        {
            Id = id;
            RoomTypeId = roomTypeId;
        }
    }
}