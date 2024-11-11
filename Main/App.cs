
using CommonModule.Commands.Composites;

namespace Main
{
    public class App(ICommandInvoker commandInvoker)
    {
        private readonly ICommandInvoker _commandInvoker = commandInvoker;

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

                _commandInvoker.Invoke(input);
            }
        }
    }
}