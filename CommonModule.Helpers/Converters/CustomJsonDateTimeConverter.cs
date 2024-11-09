using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommonModule.Helpers.Converters
{
    public class CustomJsonDateTimeConverter(string dateFormat) : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = dateFormat;

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException("Invalid token type for date conversion.");
            }

            var dateString = reader.GetString();
            if (DateTime.TryParseExact(dateString, _dateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonException($"Invalid date format. Expected format: {_dateFormat}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
