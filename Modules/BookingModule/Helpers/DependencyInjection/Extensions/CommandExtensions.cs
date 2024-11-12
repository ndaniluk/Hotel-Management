using BookingModule.Commands.Availability;
using BookingModule.Commands.Search;
using CommonModule.Commands.Composites;
using Microsoft.Extensions.DependencyInjection;

namespace BookingModule.Helpers.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<ICommandFactory, AvailabilityCommandFactory>()
                .AddTransient<ICommandFactory, SearchCommandFactory>()
                .AddTransient<IAvailabilityCommand, AvailabilityCommand>()
                .AddTransient<ISearchCommand, SearchCommand>();
        }
    }
}
