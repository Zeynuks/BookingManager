using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Property
    {
        private readonly HashSet<RoomType> _roomTypes = [ ];

        public int Id { get; private init; }
        public string Name { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public Currency Currency { get; private set; }

        public virtual  IReadOnlyCollection<RoomType> RoomTypes => _roomTypes;

        protected Property() { }

        public Property( string name, string country, string city, string address,
            decimal latitude, decimal longitude, Currency currency,
            IEnumerable<RoomType>? roomTypes = null )
        {
            Name = ValidateNonEmpty( name, "Property name" );
            Country = ValidateNonEmpty( country, "Country" );
            City = ValidateNonEmpty( city, "City" );
            Address = ValidateNonEmpty( address, "Address" );
            Latitude = ValidateLatitude( latitude );
            Longitude = ValidateLongitude( longitude );
            Currency = ValidateCurrency( currency );

            if ( roomTypes == null )
            {
                return;
            }

            foreach ( RoomType roomType in roomTypes )
            {
                AddRoomType( roomType );
            }
        }

        public void Update(
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            Currency currency )
        {
            Name = ValidateNonEmpty( name, "Property name" );
            Country = ValidateNonEmpty( country, "Country" );
            City = ValidateNonEmpty( city, "City" );
            Address = ValidateNonEmpty( address, "Address" );
            Latitude = ValidateLatitude( latitude );
            Longitude = ValidateLongitude( longitude );
            Currency = ValidateCurrency( currency );
        }

        public void AddRoomType( RoomType roomType )
        {
            _roomTypes.Add( roomType );
        }

        private static string ValidateNonEmpty( string value, string field )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                throw new DomainValidationException( $"{field} cannot be empty." );
            }

            return value.Trim();
        }

        private static decimal ValidateLatitude( decimal value )
        {
            if ( value is < -90 or > 90 )
            {
                throw new DomainValidationException( "Latitude must be between -90 and 90." );
            }

            return value;
        }

        private static decimal ValidateLongitude( decimal value )
        {
            if ( value is < -180 or > 180 )
            {
                throw new DomainValidationException( "Longitude must be between -180 and 180." );
            }

            return value;
        }

        private static Currency ValidateCurrency( Currency currency )
        {
            if ( !Enum.IsDefined( typeof( Currency ), currency ) )
            {
                throw new DomainValidationException( "Invalid currency value." );
            }

            return currency;
        }
    }
}