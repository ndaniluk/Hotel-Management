using BookingModule.Models;
using CommonModule.DataProviders.Json;
using CommonModule.Repositories;

namespace BookingModule.Repositories.Bookings
{
    public class BookingRepository(IJsonDataProvider jsonDataProvider) : BaseRepository<Booking>(jsonDataProvider), IBookingRepository
    {
    }
}
