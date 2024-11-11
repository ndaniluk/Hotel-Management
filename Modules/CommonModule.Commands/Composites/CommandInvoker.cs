
using CommonModule.Commands.Helpers;

namespace CommonModule.Commands.Composites
{
    public class CommandInvoker(ICompositeCommandFactory commandFactory) : ICommandInvoker
    {
        private readonly ICommandFactory _commandFactory = commandFactory;

        public void Invoke(string input)
        {
            var commandParts = CommandExtractor.Extract(input);

            if (_commandFactory.CanHandle(commandParts.Item1))
            {
                var command = _commandFactory.CreateCommand(commandParts.Item1);
                command.Execute(commandParts.Item2);
            }
            else
            {
                Console.WriteLine("Couldn't recognize command.");
            }
        }
    }
}
