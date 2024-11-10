using CommonModule.Helpers.Converters;
using System.Text.Json;

namespace UnitTests.Modules.CommonModule.Helpers
{
    [TestClass]
    public class CustomDateTimeConverterTests
    {
        private const string _dateTimeString = "[\"20240901\", \"20241105\"]";

        [TestMethod]
        public void Deserialize_ShouldDeserializeDatesFromString()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new CustomJsonDateTimeConverter("yyyyMMdd") }
            };

            var data = JsonSerializer.Deserialize<List<DateTime>>(_dateTimeString, options);

            Assert.IsNotNull(data);
            Assert.AreEqual(new DateTime(2024, 9, 1), data[0]);
            Assert.AreEqual(new DateTime(2024, 11, 5), data[1]);
        }
    }
}
