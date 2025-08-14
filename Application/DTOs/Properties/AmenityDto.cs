using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class AmenityDto
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; init; }

        protected AmenityDto( string name )
        {
            Name = name;
        }
    }

    public class AmenityCreateDto : AmenityDto
    {
        public AmenityCreateDto( string name ) : base( name )
        {
        }
    }

    public class AmenityReadDto : AmenityDto
    {
        [Required]
        public int Id { get; init; }

        public AmenityReadDto( int id, string name ) : base( name )
        {
            Id = id;
        }
    }

    public class AmenityUpdateDto : AmenityDto
    {
        public AmenityUpdateDto( string name ) : base( name )
        {
        }
    }
}