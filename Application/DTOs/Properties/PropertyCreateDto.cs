namespace Application.DTOs.Properties
{
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
}