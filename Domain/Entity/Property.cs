namespace Domain.Entity
{
    public class Property
    {
        public int Id { get; private init; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public string Currency { get; private set; }

        public Property(
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            string currency )
        {
            if ( string.IsNullOrEmpty( name ) )
                throw new ArgumentException( $"'{nameof( name )}' cannot be null or empty.", nameof( name ) );
            if ( string.IsNullOrEmpty( country ) )
                throw new ArgumentException( $"'{nameof( country )}' cannot be null or empty.", nameof( country ) );
            if ( string.IsNullOrEmpty( city ) )
                throw new ArgumentException( $"'{nameof( city )}' cannot be null or empty.", nameof( city ) );
            if ( string.IsNullOrEmpty( address ) )
                throw new ArgumentException( $"'{nameof( address )}' cannot be null or empty.", nameof( address ) );
            if ( string.IsNullOrEmpty( currency ) )
                throw new ArgumentException( $"'{nameof( currency )}' cannot be null or empty.", nameof( currency ) );

            Name = name;
            Country = country;
            City = city;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Currency = currency;
        }
    }
}