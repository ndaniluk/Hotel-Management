namespace CommonModule.Commands.Composites
{
    public interface ICommandFactory
    {
        bool CanHandle(string commandName);
        ICommand CreateCommand(string commandName);
    }
}
