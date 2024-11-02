using Commands;

namespace Factories.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName);
    }
}
