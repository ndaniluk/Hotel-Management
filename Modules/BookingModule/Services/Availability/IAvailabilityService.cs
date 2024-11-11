using BookingModule.Models;

namespace BookingModule.Services.Availability
{
    public interface IAvailabilityService
    {
        int GetRoomAvailabilityForSpecifiedDateRange(string hotelId, DateTime dateFrom, DateTime? dateTo, string roomType);
        IEnumerable<AvailabilityRange> GetRoomAvailabilityForFollowingDays(string hotelId, int days, string roomType, DateTime startDate = new DateTime());
    }
}
