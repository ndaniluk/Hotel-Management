using Helpers.Converters;
using Microsoft.Extensions.Configuration;
using Services.Availability;

namespace Commands.Search
{
    public class SearchCommand(IAvailabilityService availabilityService, IConfiguration configuration) : ICommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;
        private readonly IConfiguration _configuration = configuration;

        public void Execute(string[] args)
        {
            if (args.Length == 3)
            {
                var hotelId = args[0];
                var days = int.Parse(args[1]);
                var roomType = args[2];

                var availabilityCount = _availabilityService.GetRoomAvailabilityForFollowingDays(hotelId, days, roomType);

                var dateFormat = _configuration.GetRequiredSection("DateFormat").Value;
                Console.WriteLine(availabilityCount.ToOutputString(dateFormat));
            } else
            {
                Console.WriteLine("Invalid parameters provided.");
            }

        }
    }
}
