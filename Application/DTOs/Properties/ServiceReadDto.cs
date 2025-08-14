using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class ServiceReadDto : ServiceDto
    {
        [Required]
        public int Id { get; init; }

        public ServiceReadDto( int id, string name ) : base( name )
        {
            Id = id;
        }
    }
}