using CommonModule.Commands.Composites;

namespace BookingModule.Commands.Availability
{
    public class AvailabilityCommandFactory(IAvailabilityCommand availabilityCommand) : ICommandFactory
    {
        public bool CanHandle(string commandName) => commandName.Equals("Availability", StringComparison.OrdinalIgnoreCase);

        public ICommand CreateCommand(string commandName) => availabilityCommand;
    }
}
