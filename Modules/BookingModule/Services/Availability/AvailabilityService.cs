using BookingModule.Models;
using BookingModule.Repositories.Bookings;
using BookingModule.Repositories.Hotels;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BookingModule.Services.Availability
{
    public class AvailabilityService(IConfiguration configuration, IBookingRepository bookingRepository, IHotelRepository hotelRepository) : IAvailabilityService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IBookingRepository _bookingRepository = bookingRepository;
        private readonly IHotelRepository _hotelRepository = hotelRepository;

        public IEnumerable<AvailabilityRange> GetRoomAvailabilityForFollowingDays(string hotelId, int days, string roomType, DateTime startDate = default)
        {
            var hotels = _hotelRepository.GetAll();

            var roomsCount = GetRoomsCount(hotels, hotelId, roomType);
            if (roomsCount == 0)
            {
                return [];
            }

            var bookings = _bookingRepository.GetAll().Where(b => b.HotelId.Equals(hotelId, StringComparison.OrdinalIgnoreCase) && b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).OrderBy(b => b.Arrival).ThenBy(b => b.Departure);
            startDate = startDate == default ? DateTime.Now.Date : startDate.Date;

            var bookingChangesForFollowingDays = new List<KeyValuePair<DateTime, int>>();
            for (var day = startDate; day <= startDate.AddDays(days); day = day.AddDays(1))
            {
                var bookingsForDate = bookings
                    .Where(b => b.HotelId.Equals(hotelId, StringComparison.OrdinalIgnoreCase) && b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase) && IsBookingOverlapping(b, day))
                    .Count();
                var availableRooms = roomsCount - bookingsForDate;
                if (bookingChangesForFollowingDays.Count == 0 || availableRooms != bookingChangesForFollowingDays.Last().Value)
                {
                    bookingChangesForFollowingDays.Add(new KeyValuePair<DateTime, int>(day, availableRooms));
                }
            }
            var availabilityRanges = new List<AvailabilityRange>();

            for (var i = 0; i < bookingChangesForFollowingDays.Count; i++)
            {
                if (bookingChangesForFollowingDays[i].Value > 0)
                {
                    availabilityRanges.Add(
                    new AvailabilityRange(
                        bookingChangesForFollowingDays[i].Key,
                        bookingChangesForFollowingDays.Count == i + 1 ?
                            startDate.AddDays(days) :
                            bookingChangesForFollowingDays[i + 1].Key.AddDays(-1),
                        bookingChangesForFollowingDays[i].Value)
                    );
                }
            }

            return availabilityRanges;
        }

        public int GetRoomAvailabilityForSpecifiedDateRange(string hotelId, DateTime dateFrom, DateTime? dateTo, string roomType)
        {
            var hotels = _hotelRepository.GetAll();

            var roomsCount = GetRoomsCount(hotels, hotelId, roomType);
            if (roomsCount == 0)
            {
                return 0;
            }

            var bookings = _bookingRepository.GetAll();

            var bookingsForDate = bookings
                .Where(b => b.HotelId.Equals(hotelId, StringComparison.OrdinalIgnoreCase) && b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase) && IsBookingOverlapping(b, dateFrom, dateTo))
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

        private int GetRoomsCount(IEnumerable<Hotel> hotels, string hotelId, string roomType)
        {
            return hotels.Where(h => h.Id.Equals(hotelId, StringComparison.OrdinalIgnoreCase)).SelectMany(h => h.Rooms).Where(r => r.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).Count();
        }
    }
}
