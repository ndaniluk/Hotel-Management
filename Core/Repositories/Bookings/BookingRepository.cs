using Helpers.FileOperations;
using Microsoft.Extensions.Configuration;
using Models;

namespace Repositories.Bookings
{
    public class BookingRepository(IConfiguration configuration, IFileReader fileReader) : BaseRepository<Booking>(configuration, fileReader), IBookingRepository
    {
        public override Booking? GetById(string id)
        {
            return GetAllFromFile().FirstOrDefault(b => b.HotelId == id);
        }
    }
}
