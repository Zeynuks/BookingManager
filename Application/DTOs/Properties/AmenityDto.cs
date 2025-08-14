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
}