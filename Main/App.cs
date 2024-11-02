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
                var input = Console.ReadLine();
                
                if (input == string.Empty || input == null) {
                    Console.WriteLine("Exitting the app...");
                    return;
                } else
                {
                    var command = input.Split()[0];
                    var args = input.Split().Skip(1);
                    var a = _commandFactory.CreateCommand(command);
                    a.Execute(args);
                }
            }
        }
    }
}
