using Services.Availability;

namespace Commands.Availability
{
    public class AvailabilityCommand(IAvailabilityService availabilityService) : ICommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;

        public void Execute(string[] args)
        {
            var hotelId = args[0];
            var dates = args[1];
            var roomType = args[2];

            var availabilityCount = _availabilityService.GetRoomAvailabilityForSpecifiedDateRange(hotelId, dates, roomType);
            Console.WriteLine($"Available rooms for the specified date: {availabilityCount}");
        }
    }
}
