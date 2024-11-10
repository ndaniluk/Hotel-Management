using CommonModule.DataProviders;
using CommonModule.DataProviders.Helpers.Location;
using CommonModule.DataProviders.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Modules.CommonModule.DataProviders.Helpers.DependencyInjection.Extensions
{
    [TestClass]
    public class DataProviderDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddCommonModuleDataProviders();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddServices_ShouldRegisterJsonDataProvider()
        {
            var service = _serviceProvider.GetService<IJsonDataProvider>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is IJsonDataProvider);
        }

        [TestMethod]
        public void AddServices_ShouldRegisterDataObjectLocationResolver()
        {
            var service = _serviceProvider.GetService<IDataObjectLocationResolver>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is IDataObjectLocationResolver);
        }
    }
}
