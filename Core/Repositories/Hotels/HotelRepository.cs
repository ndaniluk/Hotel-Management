using Helpers.FileOperations;
using Microsoft.Extensions.Configuration;
using Models;

namespace Repositories.Hotels
{
    public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(IConfiguration configuration, IFileReader fileReader) : base(configuration, fileReader)
        {
        }

        public override Hotel? GetById(string id)
        {
            return GetAllFromFile().FirstOrDefault(h => h.Id == id);
        }
    }
}
