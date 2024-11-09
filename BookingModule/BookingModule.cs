using BookingModule.Helpers.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BookingModule
{
    public static class BookingModule
    {
        public static IServiceCollection AddBookingModule(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddCommands()
                .AddServices()
                .AddRepositories();
        }
    }
}
