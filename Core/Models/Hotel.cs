namespace Models
{
    public class Hotel(string id, string name, IEnumerable<RoomType> roomTypes, IEnumerable<Room> rooms)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public IEnumerable<RoomType> RoomTypes { get; set; } = roomTypes;
        public IEnumerable<Room> Rooms { get; set; } = rooms;
    }
}
