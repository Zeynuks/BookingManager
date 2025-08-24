using Domain.Exceptions;

namespace Domain.Entities
{
    public class Room
    {
        private readonly HashSet<Reservation> _reservations = [ ];
        public int Id { get; private init; }
        public int RoomTypeId { get; private set; }
        public string Number { get; private set; }

        public virtual RoomType RoomType { get; private set; }

        public virtual IReadOnlyCollection<Reservation> Reservations => _reservations;

        public Room() { }

        public Room( int roomTypeId, string roomNumber )
        {
            RoomTypeId = roomTypeId;
            Number = ValidateNonEmpty( roomNumber, "Number" );
        }

        public void Update( int roomTypeId, string roomNumber )
        {
            RoomTypeId = roomTypeId;
            Number = ValidateNonEmpty( roomNumber, "Number" );
        }

        private static string ValidateNonEmpty( string value, string field )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new DomainValidationException( $"{field} cannot be empty." );
            }

            return value.Trim();
        }
    }
}