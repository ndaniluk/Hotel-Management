using Commands.Availability;
using Commands.Search;
using Main.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class CommandDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCommands();
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
