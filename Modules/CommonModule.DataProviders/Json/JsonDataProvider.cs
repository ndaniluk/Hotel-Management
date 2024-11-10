using CommonModule.DataProviders.Helpers.Location;
using CommonModule.Helpers.Converters;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CommonModule.DataProviders.Json
{
    public class JsonDataProvider(IConfiguration configuration, IDataObjectLocationResolver dataObjectLocationResolver) : IJsonDataProvider
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IDataObjectLocationResolver _dataObjectLocationResolver = dataObjectLocationResolver;

        public IEnumerable<T> GetData<T>()
        {
            var filePath = _dataObjectLocationResolver.GetLocation<T>();

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                throw new FileNotFoundException($"The file '{filePath}' was not found.");

            try
            {
                var dateFormat = _configuration.GetRequiredSection("dateFormat").Value ?? "";
                var file = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new CustomJsonDateTimeConverter(dateFormat) }
                };

                var data = JsonSerializer.Deserialize<IEnumerable<T>>(file, options);
                return data ?? [];
            }
            catch (Exception)
            {
                Console.WriteLine($"Couldn't deserialize Json file.");
                return [];
            }
        }
    }
}