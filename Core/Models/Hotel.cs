namespace Models
{
    public class Hotel (string id, string name, RoomType[] roomTypes, Room[] rooms)
    {
        public string Id { get; set; } = id;
        public string Name { get; set; } = name;
        public  RoomType[] RoomTypes { get; set; } = roomTypes;
        public Room[] Rooms { get; set; } = rooms;
    }
}
