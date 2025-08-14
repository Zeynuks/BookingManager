using Domain.Exceptions;

namespace Domain.Entities
{
    public class RoomType
    {
        private readonly HashSet<Room> _rooms = [ ];
        private readonly HashSet<Service> _services = [ ];
        private readonly HashSet<Amenity> _amenities = [ ];

        public int Id { get; private init; }
        public int PropertyId { get; private set; }
        public string Name { get; private set; }
        public decimal DailyPrice { get; private set; }
        public bool IsSharedOccupancy { get; private set; }
        public int MaxPlaces { get; private set; }

        public virtual  Property Property { get; private set; }

        public virtual  IReadOnlyCollection<Room> Rooms => _rooms;
        public virtual  IReadOnlyCollection<Service> Services => _services;
        public virtual  IReadOnlyCollection<Amenity> Amenities => _amenities;

        protected RoomType() { }

        public RoomType(
            string name,
            decimal dailyPrice,
            int maxPersonCount,
            bool isSharedOccupancy )
        {
            Name = ValidateName( name );
            DailyPrice = dailyPrice;
            MaxPlaces = ValidateMaxPersonCount( maxPersonCount );
            IsSharedOccupancy = isSharedOccupancy;
        }

        public RoomType(
            int propertyId,
            string name,
            decimal dailyPrice,
            int maxPersonCount,
            bool isSharedOccupancy )
        {
            PropertyId = propertyId;
            Name = ValidateName( name );
            DailyPrice = dailyPrice;
            MaxPlaces = ValidateMaxPersonCount( maxPersonCount );
            IsSharedOccupancy = isSharedOccupancy;
        }

        public void Update(
            string name,
            decimal dailyPrice,
            int maxPersonCount,
            bool isSharedOccupancy )
        {
            Name = ValidateName( name );
            DailyPrice = dailyPrice;
            MaxPlaces = ValidateMaxPersonCount( maxPersonCount );
            IsSharedOccupancy = isSharedOccupancy;
        }

        public void AddRoom( Room room )
        {
            if ( room is null )
            {
                throw new DomainValidationException( "Room cannot be null." );
            }

            _rooms.Add( room );
        }

        public void AddService( Service service )
        {
            if ( service is null )
            {
                throw new DomainValidationException( "Service cannot be null." );
            }

            _services.Add( service );
        }

        public void RemoveService( Service service )
        {
            if ( service is null )
            {
                throw new DomainValidationException( "Service cannot be null." );
            }

            _services.Remove( service );
        }

        public void AddAmenity( Amenity amenity )
        {
            if ( amenity is null )
            {
                throw new DomainValidationException( "Amenity cannot be null." );
            }

            _amenities.Add( amenity );
        }

        public void RemoveAmenity( Amenity amenity )
        {
            if ( amenity is null )
            {
                throw new DomainValidationException( "Amenity cannot be null." );
            }

            _amenities.Remove( amenity );
        }

        private static string ValidateName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new DomainValidationException( "Room type name cannot be empty." );
            }

            return name.Trim();
        }

        private static int ValidateMaxPersonCount( int max )
        {
            if ( max <= 0 )
            {
                throw new DomainValidationException( "MaxPlaces must be greater than zero." );
            }

            return max;
        }
    }
}