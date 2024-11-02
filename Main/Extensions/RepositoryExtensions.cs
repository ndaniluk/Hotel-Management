using Microsoft.Extensions.DependencyInjection;
using Repositories.Booking;
using Repositories.Hotel;

namespace Main.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IBookingRepository, BookingRepository>()
                .AddScoped<IHotelRepository, HotelRepository>();
        }
    }
}
