using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Reservations
{
    public class GuestDto
    {
        [Required]
        [StringLength( 200 )]
        public string Name { get; init; }

        [Required]
        [StringLength( 50 )]
        public string PhoneNumber { get; init; }

        protected GuestDto( string name, string phoneNumber )
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}