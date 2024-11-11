using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookingModule;
using CommonModule;
using BookingModule.Commands.Search;
using BookingModule.Commands.Availability;
using CommonModule.Commands.Composites;

namespace UnitTests.Modules.CommonModule.Factories
{
    [TestClass]
    public class CommandFactoryTests
    {
        private ICompositeCommandFactory _commandFactory;

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
            _commandFactory = serviceProvider.GetRequiredService<ICompositeCommandFactory>();
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
    }
}
