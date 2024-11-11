using CommonModule.Commands.Composites;

namespace BookingModule.Commands.Search
{
    public class SearchCommandFactory(ISearchCommand searchCommand) : ICommandFactory
    {
        public bool CanHandle(string commandName) => commandName.Equals("Search", StringComparison.OrdinalIgnoreCase);

        public ICommand CreateCommand(string commandName) => searchCommand;
    }
}
