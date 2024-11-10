using BookingModule;
using BookingModule.Commands.Availability;
using BookingModule.Commands.Search;
using CommonModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Modules.BookingModule.Helpers.DependencyInjection.Extensions
{
    [TestClass]
    public class CommandDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddBookingModule()
                .AddCommonModules();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddCommands_ShouldRegisterAvailabilityCommand()
        {
            var command = _serviceProvider.GetService<AvailabilityCommand>();
            Assert.IsNotNull(command);
            Assert.IsTrue(command is AvailabilityCommand);
        }

        [TestMethod]
        public void AddCommands_ShouldRegisterSearchCommand()
        {
            var command = _serviceProvider.GetService<SearchCommand>();
            Assert.IsNotNull(command);
            Assert.IsTrue(command is SearchCommand);
        }
    }
}
