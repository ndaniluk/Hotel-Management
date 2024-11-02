using Commands;
using Commands.Availability;
using Commands.Search;
using Microsoft.Extensions.DependencyInjection;

namespace Factories.Commands
{
    public class CommandFactory(IServiceProvider serviceProvider) : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public ICommand CreateCommand(string commandName)
        {
            return commandName.Split()[0].ToLower() switch
            {
                "availability" => _serviceProvider.GetRequiredService<AvailabilityCommand>(),
                "search" => _serviceProvider.GetRequiredService<SearchCommand>(),
                _ => throw new ArgumentException("Invalid command name")
            };
        }
    }
}
