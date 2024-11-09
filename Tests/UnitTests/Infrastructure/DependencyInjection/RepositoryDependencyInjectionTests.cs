using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookingModule.Repositories.Bookings;
using BookingModule.Repositories.Hotels;
using BookingModule;
using CommonModule;

namespace UnitTests.Infrastructure.DependencyInjection
{
    [TestClass]
    public class RepositoryDependencyInjectionTests
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
        public void AddRepositories_ShouldRegisterBookingRepository()
        {
            var repository = _serviceProvider.GetService<IBookingRepository>();
            Assert.IsNotNull(repository);
            Assert.IsTrue(repository is IBookingRepository);
        }

        [TestMethod]
        public void AddRepositories_ShouldRegisterHotelRepository()
        {
            var repository = _serviceProvider.GetService<IHotelRepository>();
            Assert.IsNotNull(repository);
            Assert.IsTrue(repository is IHotelRepository);
        }
    }
}
