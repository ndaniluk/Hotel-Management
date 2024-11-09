using CommonModule.Commands;

namespace CommonModule.Factories.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName);
    }
}
