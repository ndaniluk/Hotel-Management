using Commands.Availability;
using Factories.Commands;
using Commands.Search;
using Main.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureTests.Factories
{
    [TestClass]
    public class CommandFactoryTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;
        private ICommandFactory _commandFactory;

        [TestInitialize]
        public void InitializeTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddCommands();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _commandFactory = _serviceProvider.GetService<ICommandFactory>();
        }

        [TestMethod]
        public void CreateCommand_ShouldCreateSearchCommand()
        {
            var command = _commandFactory.CreateCommand("Search arg1 arg2");
            Assert.IsNotNull(command);
            Assert.IsTrue(command is SearchCommand);
        }

        [TestMethod]
        public void CreateCommand_ShouldCreateAvailabilityCommand()
        {
            var command = _commandFactory.CreateCommand("Availability arg1 arg2");
            Assert.IsNotNull(command);
            Assert.IsTrue(command is AvailabilityCommand);
        }

        [TestMethod]
        public void CreateCommand_ShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _commandFactory.CreateCommand("InvalidCommand"));
        }
    }
}
