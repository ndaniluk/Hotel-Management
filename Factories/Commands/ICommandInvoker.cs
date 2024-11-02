namespace Factories.Commands
{
    public interface ICommandInvoker
    {
        void Invoke(string commandName, string[] commandArgs);  
    }
}
