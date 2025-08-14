using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class ServiceDto
    {
        [Required]
        [StringLength( 100 )]
        public string Name { get; init; }

        protected ServiceDto( string name )
        {
            Name = name;
        }
    }
}