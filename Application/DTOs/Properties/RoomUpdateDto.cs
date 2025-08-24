using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomUpdateDto : RoomDto
    {
        [Required]
        public int RoomTypeId { get; init; }

        public RoomUpdateDto(
            int roomTypeId,
            string number )
            : base( number )
        {
            RoomTypeId = roomTypeId;
        }
    }
}