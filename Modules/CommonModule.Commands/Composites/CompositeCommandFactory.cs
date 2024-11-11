namespace CommonModule.Commands.Composites
{
    public class CompositeCommandFactory(IEnumerable<ICommandFactory> factories) : ICompositeCommandFactory
    {
        private readonly IEnumerable<ICommandFactory> _factories = factories;

        public bool CanHandle(string commandName) => _factories.Any(f => f.CanHandle(commandName));

        public ICommand CreateCommand(string commandName)
        {
            var factory = _factories.FirstOrDefault(f => f.CanHandle(commandName));
            return factory?.CreateCommand(commandName) ?? throw new InvalidOperationException("Couldn't recognize command.");
        }
    }
}
