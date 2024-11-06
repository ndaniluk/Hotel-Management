namespace Services.Search
{
    public interface ISearchService
    {
        string GetRoomAvailabilityDateRanges(string hotelId, string days, string roomType);
    }
}
