
using BookingModule.Commands.Availability;
using BookingModule.Commands.Search;
using CommonModule.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Factories.Commands
{
    public class CommandFactory(IServiceProvider serviceProvider) : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public ICommand CreateCommand(string commandName)
        {
            return commandName.ToLower() switch
            {
                "availability" => _serviceProvider.GetRequiredService<AvailabilityCommand>(),
                "search" => _serviceProvider.GetRequiredService<SearchCommand>(),
                _ => throw new ArgumentException("Invalid command name")
            };
        }
    }
}
