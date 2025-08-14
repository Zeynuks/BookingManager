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

    public class GuestCreateDto : GuestDto
    {
        public GuestCreateDto( string name, string phoneNumber )
            : base( name, phoneNumber )
        {
        }
    }

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

    public class GuestUpdateDto : GuestDto
    {
        public GuestUpdateDto( string name, string phoneNumber )
            : base( name, phoneNumber )
        {
        }
    }
}