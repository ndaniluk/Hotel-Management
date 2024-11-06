namespace Services.Availability
{
    public interface IAvailabilityService
    {
        int GetRoomAvailability(string hotelId, string dates, string roomType);
    }
}
