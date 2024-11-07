using Models;

namespace Helpers.Converters
{
    public static class AvailabilityRangeListToStringExtension
    {
        public static string ToOutputString(this IEnumerable<AvailabilityRange> availabilityRanges, string dateFormat)
        {
            if (availabilityRanges == null)
            {
                return string.Empty;
            }

            var results = new List<string>();
            try
            {
                foreach (var availabilityRange in availabilityRanges)
                {
                    if (availabilityRange.DateFrom == availabilityRange.DateTo)
                    {
                        results.Add($"({availabilityRange.DateFrom.ToString(dateFormat)}, {availabilityRange.RoomAvailability})");
                    }
                    else
                    {
                        results.Add($"({availabilityRange.DateFrom.ToString(dateFormat)}-{availabilityRange.DateTo.ToString(dateFormat)}, {availabilityRange.RoomAvailability})");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format.");
                return string.Empty;
            }

            return string.Join(", ", results);
        }
    }
}

