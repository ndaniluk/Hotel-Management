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
            { "Repositories:Booking", "bookings.json" }
        };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestData");

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IFileReader>(new MockFileReader(dataDirectory))
                .AddServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _availabilityService = serviceProvider.GetRequiredService<IAvailabilityService>();
        }

        [TestMethod]
        public void GetAvailability_ShouldReturnNumberOfAvailableRoomsForSingleDate()
        {
            string[][] inputs = [["H1", "20240901", "SGL"], ["H1", "20240902", "SGL"], ["H2", "20240915", "SGL"], ["H3", "20240908", "STE"], ["H2", "20240911", "DEL"], ["H1", "20240903", "DBL"]];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {
                var hotelId = inputs[i][0];
                var date = inputs[i][1];
                var roomType = inputs[i][2];
                results[i] = _availabilityService.GetRoomAvailability(hotelId, date, roomType);
            }

            Assert.Equals(2, results[0]);
            Assert.Equals(0, results[1]);
            Assert.Equals(0, results[2]);
            Assert.Equals(2, results[3]);
            Assert.Equals(0, results[4]);
            Assert.Equals(-1, results[5]);
        }

        [TestMethod]
        public void GetAvailability_ShouldReturnNumberOfAvailableRoomsForDateRange()
        {
            string[][] inputs = [["H1", "20240901", "20240902", "SGL"], ["H1", "20240902", "20240904", "SGL"], ["H3", "20240905", "20240907", "DBL"], ["H2", "20240910", "20240911", "DEL"], ["H3", "20240920", "20240925", "STE"], ["H1", "20240902", "20240903", "DBL"]];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {
                var hotelId = inputs[i][0];
                var dateFrom = inputs[i][1];
                var dateTo = inputs[i][2];
                var roomType = inputs[i][3];
                results[i] = _availabilityService.GetRoomAvailability(hotelId, dateFrom, dateTo, roomType);
            }

            Assert.Equals(1, results[0]);
            Assert.Equals(0, results[1]);
            Assert.Equals(1, results[2]);
            Assert.Equals(0, results[3]);
            Assert.Equals(0, results[4]);
            Assert.Equals(-1, results[5]);
        }
    }
}
