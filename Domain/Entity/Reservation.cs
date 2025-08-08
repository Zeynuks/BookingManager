namespace Domain.Entity
{
    public class Reservation
    {
        public int Id { get; private init; }
        public int RoomTypeId { get; private set; }
        public int GuestId { get; private set; }
        public DateTime ArrivalDate { get; private set; }
        public DateTime DepartureDate { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public decimal Total { get; private set; }

        public RoomType RoomType { get; private set; }
        public Guest Guest { get; private set; }

        public Reservation(
            int roomTypeId,
            int guestId,
            DateTime arrivalDate,
            DateTime departureDate,
            DateTime arrivalTime,
            DateTime departureTime,
            decimal total )
        {
            RoomTypeId = roomTypeId;
            GuestId = guestId;
            ArrivalDate = arrivalDate;
            DepartureDate = departureDate;
            ArrivalTime = arrivalTime;
            DepartureTime = departureTime;
            Total = total;
        }
    }
}