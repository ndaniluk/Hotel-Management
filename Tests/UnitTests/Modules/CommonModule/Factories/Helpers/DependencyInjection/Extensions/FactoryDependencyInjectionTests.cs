using CommonModule.Commands;
using CommonModule.Commands.Composites;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Modules.CommonModule.Factories.Helpers.DependencyInjection.Extensions
{
    [TestClass]
    public class FactoryDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddCommonModuleCommands();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddServices_ShouldRegisterCommandInvoker()
        {
            var service = _serviceProvider.GetService<ICommandInvoker>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is CommandInvoker);
        }

        [TestMethod]
        public void AddServices_ShouldRegisterCommandFactory()
        {
            var service = _serviceProvider.GetService<ICompositeCommandFactory>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is CompositeCommandFactory);
        }
    }
}