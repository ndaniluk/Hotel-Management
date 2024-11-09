using BookingModule.Models;
using CommonModule.Helpers.FileOperations;
using CommonModule.Repositories;
using Microsoft.Extensions.Configuration;

namespace BookingModule.Repositories.Hotels
{
    public class HotelRepository(IConfiguration configuration) : BaseRepository<Hotel>(configuration), IHotelRepository
    {
    }
}
