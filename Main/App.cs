using Factories.Commands;

namespace Main
{
    internal class App
    {
        private ICommandFactory _commandFactory;

        public App(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        internal void Start()
        {
            while (true)
            {
                Console.WriteLine("Enter command or press Enter to exit:");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Exiting the app...");
                    return;
                }

                var inputParts = input.Split();
                var commandName = inputParts[0];
                var commandArgs = inputParts.Skip(1).ToArray();

                try
                {
                    var command = _commandFactory.CreateCommand(commandName);
                    command.Execute(commandArgs);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Error: Command '{commandName}' can't be recognized.");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"Error: Command '{commandName}' is recognized but is not properly configured.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }
}
