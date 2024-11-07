using Models;

namespace Services.Search
{
    public interface ISearchService
    {
        IEnumerable<AvailabilityRange> GetRoomAvailabilityDateRanges(string hotelId, int days, string roomType);
    }
}
