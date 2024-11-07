using Models;

namespace Services.Availability
{
    public interface IAvailabilityService
    {
        int GetRoomAvailabilityForSpecifiedDateRange(string hotelId, string dates, string roomType);
        IEnumerable<AvailabilityRange> GetRoomAvailabilityForFollowingDays(string hotelId, int days, string roomType);
    }
}
