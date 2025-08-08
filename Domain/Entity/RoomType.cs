namespace Domain.Entity
{
    public class RoomType
    {
        public int Id { get; private init; }
        public int PropertyId { get; private set; }
        public string Name { get; private set; }
        public decimal DailyPrice { get; private set; }
        public int MinPersonCount { get; private set; }
        public int MaxPersonCount { get; private set; }
        public string Currency { get; private set; }

        public Property Property { get; private set; }
        public ICollection<Service> Services { get; private set; } = new List<Service>();
        public ICollection<Amenity> Amenities { get; private set; } = new List<Amenity>();

        public RoomType(
            int propertyId,
            string name,
            decimal dailyPrice,
            int minPersonCount,
            int maxPersonCount,
            string currency)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            Name = name;
            DailyPrice = dailyPrice;
            MinPersonCount = minPersonCount;
            MaxPersonCount = maxPersonCount;
            Currency = currency;
            PropertyId = propertyId;
        }
    }
}