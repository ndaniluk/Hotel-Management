using BookingModule;
using BookingModule.Models;
using CommonModule;
using CommonModule.DataProviders.Helpers.Location;
using CommonModule.DataProviders.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Modules.CommonModule.DataProviders.Json
{
    [TestClass]
    public class JsonDataProviderTests
    {
        private IJsonDataProvider _jsonDataProvider;
        private IConfiguration _configuration;
        private IDataObjectLocationResolver _locationResolver;

        [TestInitialize]
        public void InitializeTests()
        {
            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData");

            var inMemorySettings = new Dictionary<string, string>
            {
                { "hotels", Path.Combine(dataDirectory, "hotels.json") },
                { "bookings", Path.Combine(dataDirectory, "bookings.json") },
                { "invalid", Path.Combine(dataDirectory, "invalid.json") },
                { "dateFormat", "yyyyMMdd" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddBookingModule()
                .AddCommonModules();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _jsonDataProvider = serviceProvider.GetRequiredService<IJsonDataProvider>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _locationResolver = serviceProvider.GetRequiredService<IDataObjectLocationResolver>();
        }

        [TestMethod]
        public void ReadFile_ShouldReturnListObjects()
        {
            var hotels = _jsonDataProvider.GetData<Hotel>();
            var bookings = _jsonDataProvider.GetData<Booking>();

            Assert.IsNotNull(hotels);
            Assert.IsNotNull(bookings);

            Assert.IsTrue(hotels is IEnumerable<Hotel>);
            Assert.IsTrue(bookings is IEnumerable<Booking>);

            Assert.IsTrue(hotels.Count() > 0);
            Assert.IsTrue(bookings.Count() > 0);
        }

        [TestMethod]
        public void ReadFile_ShouldThrowFileNotFoundException()
        {
            Assert.ThrowsException<FileNotFoundException>(() => _jsonDataProvider.GetData<InvalidObject>());
        }
    }

    public class InvalidObject()
    {
    }
}