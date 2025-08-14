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

    public class ServiceCreateDto : ServiceDto
    {
        public ServiceCreateDto( string name ) : base( name )
        {
        }
    }

    public class ServiceReadDto : ServiceDto
    {
        [Required]
        public int Id { get; init; }

        public ServiceReadDto( int id, string name ) : base( name )
        {
            Id = id;
        }
    }

    public class ServiceUpdateDto : ServiceDto
    {
        public ServiceUpdateDto( string name ) : base( name )
        {
        }
    }
}