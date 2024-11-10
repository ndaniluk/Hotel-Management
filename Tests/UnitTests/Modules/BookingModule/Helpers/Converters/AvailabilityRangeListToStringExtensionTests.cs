using BookingModule.Helpers.Converters;
using BookingModule.Models;

namespace UnitTests.Modules.BookingModule.Helpers.Converters
{
    [TestClass]
    public class AvailabilityRangeListToStringExtensionTests
    {
        [TestMethod]
        public void ConvertToString_ShouldReturnCorrectStringCombination()
        {
            var inputs = new List<List<AvailabilityRange>>
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

            var expectedResults = new string[]
            {
                "(20240901, 2), (20240902-20240905, 1), (20240906-20241001, 2)",
                "(20240901, 1), (20240905-20240911, 2)",
                "(20240901-20240909, 2), (20240910-20240912, 1), (20240913-20240921, 2)",
                "(20240901-20240914, 1)",
                "(20240901-20240904, 2), (20240905-20240907, 1), (20240908-20240926, 2)",
                "(20240901-20240907, 2), (20240908-20240912, 1), (20240913-20240919, 2), (20240920-20240925, 1), (20240926-20241021, 2)"
            };

            for (var inputIndex = 0; inputIndex < inputs.Count; inputIndex++)
            {
                var actualResult = inputs.ElementAt(inputIndex).ToOutputString("yyyyMMdd");
                Assert.AreEqual(expectedResults[inputIndex], actualResult);
            }
        }
    }
}
