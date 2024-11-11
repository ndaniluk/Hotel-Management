using BookingModule.Helpers.Converters;
using BookingModule.Services.Availability;
using Microsoft.Extensions.Configuration;

namespace BookingModule.Commands.Search
{
    public class SearchCommand(IConfiguration configuration, IAvailabilityService availabilityService) : ISearchCommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;
        private readonly IConfiguration _configuration = configuration;

        public void Execute(string[] args)
        {
            if (args.Length == 3 && int.TryParse(args[1], out var days))
            {
                var hotelId = args[0];
                var roomType = args[2];

                var availabilityRanges = _availabilityService.GetRoomAvailabilityForFollowingDays(hotelId, days, roomType);

                var dateFormat = _configuration.GetRequiredSection("dateFormat").Value;
                Console.WriteLine(availabilityRanges.ToOutputString(dateFormat));
            }
            else
            {
                Console.WriteLine("Invalid parameters provided.");
            }

        }
    }
}
