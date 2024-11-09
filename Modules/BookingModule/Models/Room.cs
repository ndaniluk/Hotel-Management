namespace BookingModule.Models
{
    public class Room(string roomId, string roomType)
    {
        public string RoomId { get; set; } = roomId;
        public string RoomType { get; set; } = roomType;
    }
}
