namespace Domain.Entity
{
    public class Guest
    {
        public int Id { get; private init; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();

        public Guest( string name, string phoneNumber )
        {
            if ( string.IsNullOrEmpty( name ) )
                throw new ArgumentException( $"'{nameof( name )}' cannot be null or empty.", nameof( name ) );
            if ( string.IsNullOrEmpty( phoneNumber ) )
                throw new ArgumentException( $"'{nameof( phoneNumber )}' cannot be null or empty.",
                    nameof( phoneNumber ) );

            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}