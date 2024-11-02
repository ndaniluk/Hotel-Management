using Commands.Availability;
using Commands.Search;
using Main.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureTests.DependencyInjection
{
    [TestClass]
    public class CommandDependencyInjectionTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddCommands();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
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
