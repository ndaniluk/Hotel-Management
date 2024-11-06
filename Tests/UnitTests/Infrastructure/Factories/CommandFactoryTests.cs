using Commands.Availability;
using Factories.Commands;
using Commands.Search;
using Main.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace UnitTests.Infrastructure.Factories
{
    [TestClass]
    public class CommandFactoryTests
    {
        private ICommandFactory _commandFactory;

        [TestInitialize]
        public void InitializeTests()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddServices()
                .AddCommands()
                .AddRepositories()
                .AddHelpers();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
        }

        [TestMethod]
        public void CreateCommand_ShouldCreateSearchCommand()
        {
            var command = _commandFactory.CreateCommand("Search");
            Assert.IsNotNull(command);
            Assert.IsTrue(command is SearchCommand);
        }

        [TestMethod]
        public void CreateCommand_ShouldCreateAvailabilityCommand()
        {
            var command = _commandFactory.CreateCommand("Availability");
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
