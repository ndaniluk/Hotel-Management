    using Helpers.FileOperations;
using Main.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Availability;
using UnitTests.Mocks;

namespace UnitTests.Services
{
    [TestClass]
    public class AvailabilityServiceTests
    {
        private IAvailabilityService _availabilityService;

        [TestInitialize]
        public void InitializeTests()
        {
            var inMemorySettings = new Dictionary<string, string?>
        {
            { "Repositories:Hotel", "hotels.json" },
            { "Repositories:Booking", "bookings.json" },
            { "DateFormat", "yyyyMMdd" }
        };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestData");

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IFileReader>(new MockFileReader(dataDirectory))
                .AddServices()
                .AddRepositories();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _availabilityService = serviceProvider.GetRequiredService<IAvailabilityService>();
        }

        [TestMethod]
        public void GetAvailability_ShouldReturnNumberOfAvailableRoomsForSingleDate()
        {
            string[][] inputs =
                [
                    ["H1", "20240901", "SGL"],
                    ["H1", "20240902", "SGL"],
                    ["H2", "20240915", "SGL"],
                    ["H3", "20240908", "STE"],
                    ["H2", "20240911", "DEL"],
                    ["H1", "20240903", "DBL"]
                ];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {   
                var hotelId = inputs[i][0];
                var date = inputs[i][1];
                var roomType = inputs[i][2];
                results[i] = _availabilityService.GetRoomAvailability(hotelId, date, roomType);
            }

            Assert.AreEqual(2, results[0]);
            Assert.AreEqual(1, results[1]);
            Assert.AreEqual(0, results[2]);
            Assert.AreEqual(1, results[3]);
            Assert.AreEqual(1, results[4]);
            Assert.AreEqual(-1, results[5]);
        }

        [TestMethod]
        public void GetAvailability_ShouldReturnNumberOfAvailableRoomsForDateRange()
        {
            string[][] inputs =
                [
                    ["H1", "20240901-20240902", "SGL"],
                    ["H1", "20240902-20240905", "SGL"],
                    ["H3", "20240905-20240907", "DBL"],
                    ["H2", "20240910-20240911", "DEL"],
                    ["H3", "20240920-20240925", "STE"],
                    ["H1", "20240902-20240903", "DBL"]
                ];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {
                var hotelId = inputs[i][0];
                var dates = inputs[i][1];
                var roomType = inputs[i][2];
                results[i] = _availabilityService.GetRoomAvailability(hotelId, dates, roomType);
            }

            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(1, results[1]);
            Assert.AreEqual(1, results[2]);
            Assert.AreEqual(1, results[3]);
            Assert.AreEqual(1, results[4]);
            Assert.AreEqual(-1, results[5]);
        }
    }
}
