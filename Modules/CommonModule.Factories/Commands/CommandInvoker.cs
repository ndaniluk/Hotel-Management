using CommonModule.Factories.Helpers;

namespace CommonModule.Factories.Commands
{
    public class CommandInvoker(ICommandFactory commandFactory) : ICommandInvoker
    {
        private readonly ICommandFactory _commandFactory = commandFactory;

        public void Invoke(string input)
        {
            try
            {
                var extractedInput = CommandValidator.ExtractCommand(input);
                var command = _commandFactory.CreateCommand(extractedInput.Item1);
                command.Execute(extractedInput.Item2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Invalid input. {e.Message}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"Command is recognized but is not properly configured.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
