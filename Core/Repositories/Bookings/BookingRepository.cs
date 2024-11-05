using Helpers.FileOperations;
using Microsoft.Extensions.Configuration;
using Models;

namespace Repositories.Bookings
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(IConfiguration configuration, IFileReader fileReader) : base(configuration, fileReader)
        {
        }

        public override Booking? GetById(string id)
        {
            return GetAllFromFile().FirstOrDefault(b => b.HotelId == id);
        }

        public override IEnumerable<Booking> GetAll()
        {
            return GetAllFromFile();
        }
    }
}
