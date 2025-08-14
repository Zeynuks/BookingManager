using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Properties
{
    public class PropertyDto
    {
        [Required]
        [StringLength( 200 )]
        public string Name { get; init; }

        [Required]
        [StringLength( 100 )]
        public string Country { get; init; }

        [Required]
        [StringLength( 100 )]
        public string City { get; init; }

        [Required]
        [StringLength( 300 )]
        public string Address { get; init; }

        [Required]
        [JsonRequired]
        [Range( -90, 90 )]
        public decimal Latitude { get; init; }

        [Required]
        [JsonRequired]
        [Range( -180, 180 )]
        public decimal Longitude { get; init; }

        [Required]
        [DefaultValue( "RUB" )]
        [StringLength( 3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter ISO code." )]
        public string Currency { get; init; }

        protected PropertyDto(
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            string currency )
        {
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Currency = currency;
        }
    }

    public class PropertyCreateDto : PropertyDto
    {
        public PropertyCreateDto(
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            string currency )
            : base( name, country, city, address, latitude, longitude, currency )
        {
        }
    }

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

    public class PropertyUpdateDto : PropertyDto
    {
        public PropertyUpdateDto(
            string name,
            string country,
            string city,
            string address,
            decimal latitude,
            decimal longitude,
            string currency )
            : base( name, country, city, address, latitude, longitude, currency )
        {
        }
    }
}