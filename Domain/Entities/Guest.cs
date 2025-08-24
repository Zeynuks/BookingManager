using Domain.Exceptions;

namespace Domain.Entities
{
    public class Guest
    {
        private readonly HashSet<Reservation> _reservations = [ ];

        public int Id { get; private init; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public virtual IReadOnlyCollection<Reservation> Reservations => _reservations;

        public Guest( string name, string phoneNumber )
        {
            Name = ValidateName( name );
            PhoneNumber = ValidatePhone( phoneNumber );
        }

        public void Update( string name, string phoneNumber )
        {
            Name = ValidateName( name );
            PhoneNumber = ValidatePhone( phoneNumber );
        }

        private static string ValidateName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new DomainValidationException( "Guest name cannot be empty." );
            }

            return name.Trim();
        }

        private static string ValidatePhone( string phoneNumber )
        {
            if ( string.IsNullOrWhiteSpace( phoneNumber ) )
            {
                throw new DomainValidationException( "Guest phone number cannot be empty." );
            }

            return phoneNumber.Trim();
        }
    }
}