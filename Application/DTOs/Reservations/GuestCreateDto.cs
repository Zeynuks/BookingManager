namespace Application.DTOs.Reservations
{
    public class GuestCreateDto : GuestDto
    {
        public GuestCreateDto( string name, string phoneNumber )
            : base( name, phoneNumber )
        {
        }
    }
}