using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class RoomDto
    {
        [Required]
        [StringLength( 50 )]
        public string Number { get; init; }

        protected RoomDto( string number )
        {
            Number = number;
        }
    }

    public class RoomCreateDto : RoomDto
    {
        public RoomCreateDto( string number )
            : base( number )
        {
        }
    }

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

    public class RoomBookingDto
    {
        public RoomBookingDto()
        {
        }
    }
}