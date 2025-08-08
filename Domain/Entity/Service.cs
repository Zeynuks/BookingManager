namespace Domain.Entity
{
    public class Service
    {
        public int Id { get; private init; }
        public string Name { get; private set; }

        public ICollection<RoomType> RoomTypes { get; private set; } = new List<RoomType>();

        public Service(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));

            Name = name;
        }
    }
}