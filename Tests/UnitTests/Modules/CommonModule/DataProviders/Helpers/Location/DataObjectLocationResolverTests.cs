using BookingModule.Models;
using BookingModule.Services.Availability;
using CommonModule;
using CommonModule.DataProviders.Helpers.Location;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Modules.CommonModule.DataProviders.Helpers.Location
{
    [TestClass]
    public class DataObjectLocationResolverTests
    {
        private IDataObjectLocationResolver _dataObjectLocationResolver;

        [TestInitialize]
        public void InitializeTests()
        {
            var inMemorySettings = new Dictionary<string, string>
        {
            { "hotels", "hotels.json" },
            { "bookings", "bookings.json" },
        };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddCommonModules()
                .AddSingleton<IConfiguration>(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _dataObjectLocationResolver = serviceProvider.GetRequiredService<IDataObjectLocationResolver>();
        }

        [TestMethod]
        public void ResolveLocation_ShouldReturnDataObjectLocation()
        {
            var hotelsLocation = _dataObjectLocationResolver.GetLocation<Hotel>();
            var bookingsLocation = _dataObjectLocationResolver.GetLocation<Booking>();

            Assert.AreEqual("hotels.json", hotelsLocation);
            Assert.AreEqual("bookings.json", bookingsLocation);
        }
    }
}
