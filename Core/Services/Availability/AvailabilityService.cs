using Models;
using Repositories.Bookings;

namespace Services.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IBookingRepository _bookingRepository;

        public AvailabilityService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
    }
}
