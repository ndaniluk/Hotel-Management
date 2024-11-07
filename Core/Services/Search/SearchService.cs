using Models;

namespace Services.Search
{
    public class SearchService : ISearchService
    {
        public IEnumerable<AvailabilityRange> GetRoomAvailabilityDateRanges(string hotelId, int days, string roomType)
        {
            return new List<AvailabilityRange>();
        }
    }
}
