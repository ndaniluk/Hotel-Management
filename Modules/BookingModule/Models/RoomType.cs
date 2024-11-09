namespace BookingModule.Models
{
    public class RoomType(string code, string description, string[] amenties, string[] features)
    {
        public string Code { get; set; } = code;
        public string Description { get; set; } = description;
        public string[] Amenties { get; set; } = amenties ?? [];
        public string[] Features { get; set; } = features ?? [];
    }
}
