using Models;

namespace Helpers.Converters
{
    public static class AvailabilityRangeListToStringExtension
    {
        public static string ToOutputString(this IEnumerable<AvailabilityRange> availabilityRanges)
        {
            if (availabilityRanges == null)
            {
                return string.Empty;
            }

            var results = new List<string>();
            foreach (var availabilityRange in availabilityRanges)
            {
                if (availabilityRange.DateFrom == availabilityRange.DateTo)
                {
                    results.Add($"({availabilityRange.DateFrom:yyyyMMdd}, {availabilityRange.RoomAvailability})");
                }
                else
                {
                    results.Add($"({availabilityRange.DateFrom:yyyyMMdd}-{availabilityRange.DateTo:yyyyMMdd}, {availabilityRange.RoomAvailability})");
                }
            }

            return string.Join(", ", results);
        }
    }
}

