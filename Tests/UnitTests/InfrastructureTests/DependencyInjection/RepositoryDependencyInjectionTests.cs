using Microsoft.Extensions.DependencyInjection;
using Repositories.Bookings;
using Repositories.Hotels;
using Main.Extensions;

namespace InfrastructureTests.DependencyInjection
{
    [TestClass]
    public class RepositoryDependencyInjectionTests
    {
        private IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitializeTests()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddRepositories();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
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
