using Microsoft.Extensions.DependencyInjection;
using Services.Availability;
using Main.Extensions;
using Services.Search;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class ServiceDependencyInjectionTests
    {
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices();
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
