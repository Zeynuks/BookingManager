using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Properties
{
    public class PropertyReadDto : PropertyDto
    {
        [Required]
        public int Id { get; init; }

        public PropertyReadDto(
            int id,
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            string currency )
            : base( name, country, city, address, latitude, longitude, currency )
        {
            Id = id;
        }
    }
}