using BookingModule.Models;
using CommonModule.Helpers.FileOperations;
using CommonModule.Repositories;
using Microsoft.Extensions.Configuration;

namespace BookingModule.Repositories.Bookings
{
    public class BookingRepository(IConfiguration configuration) : BaseRepository<Booking>(configuration), IBookingRepository
    {
    }
}
