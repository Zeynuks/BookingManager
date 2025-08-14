using Domain.Exceptions;

namespace Domain.Entities
{
    public class Amenity
    {
        public int Id { get; private init; }
        public string Name { get; private set; }

        public virtual  ICollection<RoomType> RoomTypes { get; private set; } = new List<RoomType>();

        public Amenity( string name )
        {
            Name = ValidateName( name );
        }

        public void Update( string name )
        {
            Name = ValidateName( name );
        }

        private static string ValidateName( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new DomainValidationException( "Amenity name cannot be empty." );
            }

            return name.Trim();
        }
    }
}