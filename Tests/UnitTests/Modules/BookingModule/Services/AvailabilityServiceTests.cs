using BookingModule.Services.Availability;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingModule;
using CommonModule;
using BookingModule.Models;

namespace UnitTests.Modules.BookingModule.Services
{
    [TestClass]
    public class AvailabilityServiceTests
    {
        private IAvailabilityService _availabilityService;

        [TestInitialize]
        public void InitializeTests()
        {
            var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TestData");

            var inMemorySettings = new Dictionary<string, string>
            {
                { "hotels", Path.Combine(dataDirectory, "hotels.json") },
                { "bookings", Path.Combine(dataDirectory, "bookings.json") },
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
            _availabilityService = serviceProvider.GetRequiredService<IAvailabilityService>();
        }

        [TestMethod]
        public void GetAvailability_ShouldReturnNumberOfAvailableRoomsForSingleDate()
        {
            string[][] inputs =
                [
                    ["H1", "20240901", "SGL"],
                    ["H1", "20240902", "Sgl"],
                    ["h2", "20240915", "SGL"],
                    ["H3", "20240908", "stE"],
                    ["h2", "20240911", "DEL"],
                    ["H1", "20240903", "DbL"]
                ];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {
                var hotelId = inputs[i][0];
                var date = inputs[i][1];
                var roomType = inputs[i][2];
                results[i] = _availabilityService.GetRoomAvailabilityForSpecifiedDateRange(hotelId, DateTime.ParseExact(date, "yyyyMMdd", null), null, roomType);
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
                    ["h1", "20240902-20240905", "sgL"],
                    ["H3", "20240905-20240907", "DBL"],
                    ["h2", "20240910-20240911", "Del"],
                    ["h3", "20240920-20240925", "StE"],
                    ["H1", "20240902-20240903", "DBL"]
                ];
            var results = new int[6];

            for (int i = 0; i < inputs.Length; i++)
            {
                var hotelId = inputs[i][0];
                var dates = inputs[i][1];
                var datesRange = dates.Split('-');
                var roomType = inputs[i][2];
                results[i] = _availabilityService.GetRoomAvailabilityForSpecifiedDateRange(hotelId, DateTime.ParseExact(datesRange[0], "yyyyMMdd", null), DateTime.ParseExact(datesRange[1], "yyyyMMdd", null), roomType);
            }

            Assert.AreEqual(1, results[0]);
            Assert.AreEqual(1, results[1]);
            Assert.AreEqual(1, results[2]);
            Assert.AreEqual(1, results[3]);
            Assert.AreEqual(1, results[4]);
            Assert.AreEqual(-1, results[5]);
        }

        [TestMethod]
        public void Search_ShouldReturnAvailableBookingsWithinFollowingDays()
        {
            string[][] inputs =
            [
                [ "h1", "30", "sGL"],
                [ "h1", "10", "DBl" ],
                [ "H2", "20", "Del" ],
                [ "h2", "15", "SGL" ],
                [ "H3", "25", "dbL" ],
                [ "H3", "50", "STE" ]
            ];

            var expectedResults = new List<List<AvailabilityRange>>
            {
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240901", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240902", "yyyyMMdd", null), DateTime.ParseExact("20240905", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240906", "yyyyMMdd", null), DateTime.ParseExact("20241001", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240901", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240905", "yyyyMMdd", null), DateTime.ParseExact("20240911", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240909", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240910", "yyyyMMdd", null), DateTime.ParseExact("20240912", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240913", "yyyyMMdd", null), DateTime.ParseExact("20240921", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240914", "yyyyMMdd", null), 1)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240904", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240905", "yyyyMMdd", null), DateTime.ParseExact("20240907", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240908", "yyyyMMdd", null), DateTime.ParseExact("20240926", "yyyyMMdd", null), 2)
                },
                new() {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240907", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240908", "yyyyMMdd", null), DateTime.ParseExact("20240912", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240913", "yyyyMMdd", null), DateTime.ParseExact("20240919", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240920", "yyyyMMdd", null), DateTime.ParseExact("20240925", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240926", "yyyyMMdd", null), DateTime.ParseExact("20241021", "yyyyMMdd", null), 2)
                }
            };

            for (var inputIndex = 0; inputIndex < inputs.Length; inputIndex++)
            {
                var hotelId = inputs[inputIndex][0];
                var numberOfDays = int.Parse(inputs[inputIndex][1]);
                var roomType = inputs[inputIndex][2];

                var actualResults = _availabilityService.GetRoomAvailabilityForFollowingDays(hotelId, numberOfDays, roomType, DateTime.ParseExact("20240901", "yyyyMMdd", null));

                for (var resultIndex = 0; resultIndex < expectedResults[inputIndex].Count; resultIndex++)
                {
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].DateFrom, actualResults.ElementAt(resultIndex).DateFrom);
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].DateTo, actualResults.ElementAt(resultIndex).DateTo);
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].RoomAvailability, actualResults.ElementAt(resultIndex).RoomAvailability);
                }
            }
        }
    }
}
