using Factories.Commands;
using Microsoft.Extensions.Configuration;

namespace Main
{
    internal class App
    {
        private readonly ICommandInvoker _commandInvoker;
        private readonly IConfiguration _configuration;

        public App(ICommandInvoker commandInvoker, IConfiguration configuration)
        {
            _commandInvoker = commandInvoker;
            _configuration = configuration;
        }

        internal void Start()
        {
            while (true)
            {
                var hotels = _configuration.GetSection("Repositories").GetSection("Hotels").Value;
                Console.WriteLine(hotels);
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