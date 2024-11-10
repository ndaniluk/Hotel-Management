using BookingModule.Repositories.Bookings;
using BookingModule.Repositories.Hotels;
using Microsoft.Extensions.DependencyInjection;

namespace BookingModule.Helpers.DependencyInjection.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IBookingRepository, BookingRepository>()
                .AddTransient<IHotelRepository, HotelRepository>();
        }
    }
}
