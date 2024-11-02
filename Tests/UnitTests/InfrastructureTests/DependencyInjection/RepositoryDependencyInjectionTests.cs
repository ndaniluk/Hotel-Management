using Microsoft.Extensions.DependencyInjection;
using Repositories.Booking;
using Repositories.Hotel;
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
        public void AddServices_ShouldRegisterBookingRepository()
        {
            var repository = _serviceProvider.GetService<IBookingRepository>();    
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void AddServices_ShouldRegisterHotelRepository()
        {
            var repository = _serviceProvider.GetService<IHotelRepository>();
            Assert.IsNotNull(repository);
        }
    }
}
