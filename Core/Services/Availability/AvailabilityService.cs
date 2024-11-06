using Microsoft.Extensions.Configuration;
using Models;
using Repositories.Bookings;
using Repositories.Hotels;
using System.Globalization;

namespace Services.Availability
{
    public class AvailabilityService(IConfiguration configuration, IBookingRepository bookingRepository, IHotelRepository hotelRepository) : IAvailabilityService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IHotelRepository _hotelRepository = hotelRepository;

        public int GetRoomAvailability(string hotelId, string dates, string roomType)
        {
            var datesRange = dates.Split('-');
            var dateFrom = datesRange[0];
            var dateTo = datesRange.Length > 1 ? datesRange[1] : null;
            return GetRoomAvailability(hotelId, roomType, dateFrom, dateTo);
        }

        private int GetRoomAvailability(string hotelId, string roomType, string dateFrom, string? dateTo = null)
        {
            var roomsCount = GetRoomsCount(hotelId, roomType);
            if (roomsCount == 0)
            {
                return 0;
            }

            DateTime parsedDateFrom;
            DateTime? parsedDateTo = null;

            try
            {
                parsedDateFrom = GetDate(dateFrom);
                if (dateTo != null)
                {
                    parsedDateTo = GetDate(dateTo);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format.");
                return 0;
            }

            var bookings = _bookingRepository.GetAll();

            var bookingsForDate = bookings
                .Where(b => b.HotelId == hotelId && b.RoomType == roomType && IsBookingOverlapping(b, parsedDateFrom, parsedDateTo))
                .Count();

            return roomsCount - bookingsForDate;
        }

        private bool IsBookingOverlapping(Booking booking, DateTime dateFrom, DateTime? dateTo = null)
        {
            if (dateTo == null)
            {
                return booking.Arrival <= dateFrom && booking.Departure >= dateFrom;
            }

            return (booking.Arrival <= dateFrom && booking.Departure >= dateFrom) ||
                   (booking.Arrival <= dateTo && booking.Departure >= dateTo) ||
                   (booking.Arrival >= dateFrom && booking.Departure <= dateTo);
        }

        private DateTime GetDate(string date)
        {
            var dateFormat = _configuration.GetRequiredSection("DateFormat").Value ?? "";
            return DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);
        }

        private int GetRoomsCount(string hotelId, string roomType)
        {
            var hotels = _hotelRepository.GetAll();
            return hotels.Where(h => h.Id == hotelId).SelectMany(h => h.Rooms).Where(r => r.RoomType == roomType).Count();
        }
    }
}
