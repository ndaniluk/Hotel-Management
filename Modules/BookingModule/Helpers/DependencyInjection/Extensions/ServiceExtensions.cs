using BookingModule.Services.Availability;
using Microsoft.Extensions.DependencyInjection;

namespace BookingModule.Helpers.DependencyInjection.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IAvailabilityService, AvailabilityService>();
        }
    }
}
