using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class AmenityReadDto : AmenityDto
    {
        [Required]
        public int Id { get; init; }

        public AmenityReadDto( int id, string name ) : base( name )
        {
            Id = id;
        }
    }
}