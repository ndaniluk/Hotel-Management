using Helpers.FileOperations;
using Microsoft.Extensions.Configuration;
using Models;

namespace Repositories.Bookings
{
    public class BookingRepository(IConfiguration configuration, IFileReader fileReader) : BaseRepository<Booking>(configuration, fileReader), IBookingRepository
    {
    }
}
