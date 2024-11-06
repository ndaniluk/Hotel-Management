using Microsoft.Extensions.Configuration;
using Repositories.Bookings;
using Repositories.Hotels;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IConfiguration _configuration;
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;

        public AvailabilityService(IConfiguration configuration, IBookingRepository bookingRepository, IHotelRepository hotelRepository)
        {
            _configuration = configuration;
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
        }

        public int GetRoomAvailability(string hotelId, string date, string roomType)
        {
            var roomsCount = GetRoomsCount(hotelId, roomType);
            if (roomsCount == 0)
            {
                return roomsCount;
            }

            var bookings = _bookingRepository.GetAll();

            var parsedDate = new DateTime();
            try
            {
                parsedDate = GetDate(date);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format.");
                return 0;
            }

            var bookingsForDate = bookings
                .Where(b => 
                    (b.Arrival <= parsedDate && b.Departure >= parsedDate) && 
                    b.HotelId == hotelId && b.RoomType == roomType)
                .Count();

            return roomsCount - bookingsForDate;
        }

        public int GetRoomAvailability(string hotelId, string dateFrom, string dateTo, string roomType)
        {
            var roomsCount = GetRoomsCount(hotelId, roomType);
            if (roomsCount == 0)
            {
                return roomsCount;
            }

            var bookings = _bookingRepository.GetAll();

            var parsedDateFrom = new DateTime();
            var parsedDateTo = new DateTime();

            try
            {
                parsedDateFrom = GetDate(dateFrom);
                parsedDateTo = GetDate(dateTo);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format.");
                return 0;
            }

            var bookingsForDate = bookings
                .Where(b =>
                    (
                        (b.Arrival <= parsedDateFrom && b.Departure >= parsedDateFrom) ||
                        (b.Arrival <= parsedDateTo && b.Departure >= parsedDateTo)
                    ) &&
                    b.HotelId == hotelId && b.RoomType == roomType)
                .Count();

            return roomsCount - bookingsForDate;
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
