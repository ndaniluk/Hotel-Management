using Helpers.FileOperations;
using Main.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

    //    [TestMethod]
    //    public void Search_ShouldReturnAvailableBookingsWithinFollowingDays()
    //    {
    //        string[][] inputs =
    //            [
    //                ["H1", "365", "SGL"],
    //                ["H1", "30", "SGL"],
    //                ["H2", "12", "SGL"],
    //                ["H3", "22", "STE"],
    //                ["H2", "50", "DEL"],
    //                ["H1", "150", "DBL"]
    //            ];
    //        var results = new int[6];

    //        for (int i = 0; i < inputs.Length; i++)
    //        {
    //            var hotelId = inputs[i][0];
    //            var date = inputs[i][1];
    //            var roomType = inputs[i][2];
    //            results[i] = _searchService.Get(hotelId, date, roomType);
    //        }

    //        Assert.AreEqual(2, results[0]);
    //        Assert.AreEqual(1, results[1]);
    //        Assert.AreEqual(0, results[2]);
    //        Assert.AreEqual(1, results[3]);
    //        Assert.AreEqual(1, results[4]);
    //        Assert.AreEqual(-1, results[5]);
    //    }
    //}
    }
}
