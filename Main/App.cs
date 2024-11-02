using Factories.Commands;

namespace Main
{
    internal class App
    {
        private readonly ICommandInvoker _commandInvoker;

        public App(ICommandInvoker commandInvoker)
        {
            _commandInvoker = commandInvoker;
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

                _commandInvoker.Invoke(commandName, commandArgs);
            }
        }
    }
}