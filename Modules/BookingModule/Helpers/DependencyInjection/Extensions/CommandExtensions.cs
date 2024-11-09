using BookingModule.Commands.Availability;
using BookingModule.Commands.Search;
using Microsoft.Extensions.DependencyInjection;

namespace BookingModule.Helpers.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<AvailabilityCommand>()
                .AddTransient<SearchCommand>();
        }
    }
}
