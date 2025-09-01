namespace Application.DTOs.Reservations
{
    public class GuestUpdateDto : GuestDto
    {
        public GuestUpdateDto( string name, string phoneNumber )
            : base( name, phoneNumber )
        {
        }
    }
}