namespace Factories.Commands
{
    public  class CommandInvoker(ICommandFactory commandFactory) : ICommandInvoker
    {
        private readonly ICommandFactory _commandFactory = commandFactory;

        public void Invoke(string commandName, string[] commandArgs)
        {
            try
            {
                var command = _commandFactory.CreateCommand(commandName);
                command.Execute(commandArgs);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Command '{commandName}' can't be recognized.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Command '{commandName}' is recognized but is not properly configured.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
