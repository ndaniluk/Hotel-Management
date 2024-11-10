using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CommonModule.Factories.Commands;
using BookingModule;
using CommonModule;
using BookingModule.Commands.Search;
using BookingModule.Commands.Availability;

namespace UnitTests.Modules.CommonModule.Factories
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
                .AddBookingModule()
                .AddCommonModules();
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
