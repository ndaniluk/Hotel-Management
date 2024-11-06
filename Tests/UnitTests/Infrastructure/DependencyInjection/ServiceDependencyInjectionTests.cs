using Microsoft.Extensions.DependencyInjection;
using Services.Availability;
using Main.Extensions;
using Services.Search;
using Microsoft.Extensions.Configuration;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class ServiceDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddServices()
                .AddRepositories()
                .AddHelpers();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddServices_ShouldRegisterAvailabilityService()
        {
            var service = _serviceProvider.GetService<IAvailabilityService>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is IAvailabilityService);
        }

        [TestMethod]
        public void AddServices_ShouldRegisterSearchService()
        {
            var service = _serviceProvider.GetService<ISearchService>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is ISearchService);
        }
    }
}
