using Microsoft.Extensions.Configuration;
using Helpers.FileOperations;
using System.Text.Json;
using Helpers.Converters;

namespace Repositories
{
    public abstract class BaseRepository<T>(IConfiguration configuration, IFileReader reader) : IRepository<T>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IFileReader _reader = reader;

        public virtual IEnumerable<T> GetAll()
        {
            return GetAllFromFile();
        }

        public IEnumerable<T> GetAllFromFile()
        {
            var fileName = _configuration[$"{typeof(T).Name.ToLower()}s"];
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Couldn't connect with the data provider");
                return [];
            }

            try
            {
                var dateFormat = _configuration.GetRequiredSection("dateFormat").Value ?? "";
                var file = _reader.Read(fileName);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new CustomJsonDateTimeConverter(dateFormat) }
                };

                var data = JsonSerializer.Deserialize<IEnumerable<T>>(file, options);
                return data ?? [];
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return [];
            }
        }
    }
}
