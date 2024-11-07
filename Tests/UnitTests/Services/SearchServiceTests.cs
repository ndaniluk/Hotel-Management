using Helpers.FileOperations;
using Main.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Services.Search;
using UnitTests.Mocks;

namespace UnitTests.Services
{
    [TestClass]
    public class SearchServiceTests
    {
        private ISearchService _searchService;

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
            _searchService = serviceProvider.GetRequiredService<ISearchService>();
        }

        [TestMethod]
        public void Search_ShouldReturnAvailableBookingsWithinFollowingDays()
        {
            string[][] inputs =
            [
                ["H1", "30", "SGL"],
                [ "H1", "10", "DBL" ],
                [ "H2", "20", "DEL" ],
                [ "H2", "15", "SGL" ],
                [ "H3", "25", "DBL" ],
                [ "H3", "50", "STE" ]
            ];

            var expectedResults = new List<List<AvailabilityRange>>
            {
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240901", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240902", "yyyyMMdd", null), DateTime.ParseExact("20240905", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240906", "yyyyMMdd", null), DateTime.ParseExact("20240930", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240901", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240905", "yyyyMMdd", null), DateTime.ParseExact("20240910", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240909", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240910", "yyyyMMdd", null), DateTime.ParseExact("20240912", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240913", "yyyyMMdd", null), DateTime.ParseExact("20240920", "yyyyMMdd", null), 2)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240914", "yyyyMMdd", null), 1)
                },
                new ()
                {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240904", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240905", "yyyyMMdd", null), DateTime.ParseExact("20240907", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240908", "yyyyMMdd", null), DateTime.ParseExact("20240924", "yyyyMMdd", null), 2)
                },
                new() {
                    new (DateTime.ParseExact("20240901", "yyyyMMdd", null), DateTime.ParseExact("20240907", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240908", "yyyyMMdd", null), DateTime.ParseExact("20240912", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240913", "yyyyMMdd", null), DateTime.ParseExact("20240919", "yyyyMMdd", null), 2),
                    new (DateTime.ParseExact("20240920", "yyyyMMdd", null), DateTime.ParseExact("20240925", "yyyyMMdd", null), 1),
                    new (DateTime.ParseExact("20240926", "yyyyMMdd", null), DateTime.ParseExact("20241020", "yyyyMMdd", null), 2)
                }
            };

            for (var inputIndex = 0; inputIndex < inputs.Length; inputIndex++)
            {
                var hotelId = inputs[inputIndex][0];
                var numberOfDays = int.Parse(inputs[inputIndex][1]);
                var roomType = inputs[inputIndex][2];

                var actualResults = _searchService.GetRoomAvailabilityDateRanges(hotelId, numberOfDays, roomType);

                for (var resultIndex = 0; resultIndex < expectedResults.Count; resultIndex++)
                {
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].DateFrom, actualResults.ElementAt(resultIndex).DateFrom);
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].DateTo, actualResults.ElementAt(resultIndex).DateTo);
                    Assert.AreEqual(expectedResults[inputIndex][resultIndex].RoomAvailability, actualResults.ElementAt(resultIndex).RoomAvailability);
                }
            }
        }
    }
}

