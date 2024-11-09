﻿using BookingModule.Services.Availability;
using CommonModule.Commands;

namespace BookingModule.Commands.Availability
{
    public class AvailabilityCommand(IAvailabilityService availabilityService) : ICommand
    {
        private readonly IAvailabilityService _availabilityService = availabilityService;

        public void Execute(string[] args)
        {
            if (args.Length == 3)
            {
                var hotelId = args[0];
                var dates = args[1];
                var roomType = args[2];

                var availabilityCount = _availabilityService.GetRoomAvailabilityForSpecifiedDateRange(hotelId, dates, roomType);
                Console.WriteLine($"Available rooms for the specified date: {availabilityCount}");
            } else
            {
                Console.WriteLine("Invalid parameters provided");
            }
        }
    }
}
