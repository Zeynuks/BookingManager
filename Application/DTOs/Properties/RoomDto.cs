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
}