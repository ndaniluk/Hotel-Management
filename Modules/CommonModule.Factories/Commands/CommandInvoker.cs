namespace CommonModule.Factories.Commands
{
    public  class CommandInvoker(ICommandFactory commandFactory) : ICommandInvoker
    {
        private readonly ICommandFactory _commandFactory = commandFactory;

        public void Invoke(string input)
        {
            try
            {
                int braceIndex = input.IndexOf('(');
                if (braceIndex == -1 || !input.EndsWith(")"))
                {
                    Console.WriteLine("Invalid command format. Expected format: Command(arg1, arg2, ...)");
                    return;
                }

                string commandName = input.Substring(0, braceIndex).Trim();
                if (string.IsNullOrWhiteSpace(commandName))
                {
                    Console.WriteLine("Invalid command. Command cannot be empty.");
                    return;
                }

                var argsPart = input.Substring(braceIndex + 1, input.Length - braceIndex - 2).Trim();
                var args = argsPart.Length > 0 ? argsPart.Split(", ") : [];

                var command = _commandFactory.CreateCommand(commandName);
                command.Execute(args);
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Couldn't recognize command.");
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
