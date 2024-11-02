using Commands;
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
        public void AddServices_ShouldRegisterAvailabilityCommand()
        {
            var commands = _serviceProvider.GetServices<ICommand>().ToList();
            Assert.IsTrue(commands.Any(c => c is AvailabilityCommand));
        }

        [TestMethod]
        public void AddServices_ShouldRegisterSearchCommand()
        {
            var commands = _serviceProvider.GetServices<ICommand>().ToList();
            Assert.IsTrue(commands.Any(c => c is SearchCommand));
        }
    }
}
