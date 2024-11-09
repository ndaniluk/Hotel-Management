using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookingModule.Services.Availability;
using BookingModule;
using CommonModule;

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
                .AddBookingModule()
                .AddCommonModules();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void AddServices_ShouldRegisterAvailabilityService()
        {
            var service = _serviceProvider.GetService<IAvailabilityService>();
            Assert.IsNotNull(service);
            Assert.IsTrue(service is IAvailabilityService);
        }
    }
}
