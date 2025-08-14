using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Reservations
{
    public class GuestReadDto : GuestDto
    {
        [Required]
        public int Id { get; init; }

        public GuestReadDto( int id, string name, string phoneNumber )
            : base( name, phoneNumber )
        {
            Id = id;
        }
    }
}