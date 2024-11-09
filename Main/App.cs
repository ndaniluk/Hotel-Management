using CommonModule.Factories.Commands;
using Microsoft.Extensions.Configuration;

namespace Main
{
    public class App(ICommandInvoker commandInvoker, IConfiguration configuration)
    {
        private readonly ICommandInvoker _commandInvoker = commandInvoker;
        private readonly IConfiguration _configuration = configuration;

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