using BookingModule.Models;
using CommonModule.DataProviders.Json;
using CommonModule.Repositories;

namespace BookingModule.Repositories.Hotels
{
    public class HotelRepository(IJsonDataProvider jsonDataProvider) : BaseRepository<Hotel>(jsonDataProvider), IHotelRepository
    {
    }
}
