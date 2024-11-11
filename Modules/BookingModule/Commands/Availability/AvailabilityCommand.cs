using BookingModule.Services.Availability;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BookingModule.Commands.Availability
{
    public class AvailabilityCommand(IConfiguration configuration, IAvailabilityService availabilityService) : IAvailabilityCommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;
        private readonly IConfiguration _configuration = configuration;

        public void Execute(string[] args)
        {
            if (args.Length == 3)
            {
                var hotelId = args[0];
                var roomType = args[2];

                var datesRange = args[1].Split('-');

                try
                {
                    var dateFrom = GetDate(datesRange[0]);
                    DateTime? dateTo = datesRange.Length > 1 ? GetDate(datesRange[1]) : null;

                    var availabilityCount = _availabilityService.GetRoomAvailabilityForSpecifiedDateRange(hotelId, dateFrom, dateTo, roomType);
                    Console.WriteLine($"Available rooms for the specified date: {availabilityCount}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid date format.");
                }   

            } else
            {
                Console.WriteLine("Invalid parameters provided");
            }
        }


        private DateTime GetDate(string date)
        {
            var dateFormat = _configuration.GetRequiredSection("dateFormat").Value ?? "";
            return DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);
        }
    }
}
