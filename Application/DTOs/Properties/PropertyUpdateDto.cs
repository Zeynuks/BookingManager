namespace Application.DTOs.Properties
{
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