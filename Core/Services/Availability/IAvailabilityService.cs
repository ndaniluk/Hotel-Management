namespace Services.Availability
{
    public interface IAvailabilityService
    {
        int GetRoomAvailability(string hotelId, string roomType, string date);
        int GetRoomAvailability(string hotelId, string roomType, string dateFrom, string dateTo);

    }
}
