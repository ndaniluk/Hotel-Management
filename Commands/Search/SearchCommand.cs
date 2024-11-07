using Helpers.Converters;
using Services.Availability;

namespace Commands.Search
{
    public class SearchCommand(IAvailabilityService availabilityService) : ICommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;

        public void Execute(string[] args)
        {
            var hotelId = args[0];
            var days = int.Parse(args[1]);
            var roomType = args[2];

            var availabilityCount = _availabilityService.GetRoomAvailabilityForFollowingDays(hotelId, days, roomType);
            Console.WriteLine(availabilityCount.ToOutputString());
        }
    }
}
