namespace BookingModule.Models
{
    public class Booking (string hotelId, DateTime arrival, DateTime departure, string roomType, string roomRate)
    {
        public string HotelId { get; set; } = hotelId;
        public DateTime Arrival { get; set; } = arrival;
        public DateTime Departure { get; set; } = departure;
        public string RoomType { get; set; } = roomType;
        public string RoomRate { get; set; } = roomRate;
    }
}
