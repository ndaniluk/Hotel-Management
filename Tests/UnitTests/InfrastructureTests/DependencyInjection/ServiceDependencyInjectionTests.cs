using Microsoft.Extensions.DependencyInjection;
using Services.Availability;
using Main.Extensions;
using Services.Search;


namespace InfrastructureTests.DependencyInjection
{
    [TestClass]
    public class ServiceDependencyInjectionTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddServices();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddServices_ShouldRegisterAvailabilityService()
        {
            var service = _serviceProvider.GetService<IAvailabilityService>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void AddServices_ShouldRegisterSearchService()
        {
            var service = _serviceProvider.GetService<ISearchService>();
            Assert.IsNotNull(service);
        }
    }
}
