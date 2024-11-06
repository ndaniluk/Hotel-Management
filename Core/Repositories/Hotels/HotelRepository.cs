using Helpers.FileOperations;
using Microsoft.Extensions.Configuration;
using Models;

namespace Repositories.Hotels
{
    public class HotelRepository(IConfiguration configuration, IFileReader fileReader) : BaseRepository<Hotel>(configuration, fileReader), IHotelRepository
    {
        public override Hotel? GetById(string id)
        {
            return GetAllFromFile().FirstOrDefault(h => h.Id == id);
        }
    }
}
