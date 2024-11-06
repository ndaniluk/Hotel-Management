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

        public abstract T? GetById(string id);
        public virtual IEnumerable<T> GetAll()
        {
            return GetAllFromFile();
        }

        public IEnumerable<T> GetAllFromFile()
        {
            var fileName = _configuration.GetSection("Repositories").GetSection(typeof(T).Name).Value;
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Couldn't connect with the data provider");
                return Enumerable.Empty<T>();
            }

            try
            {
                var dateFormat = _configuration.GetRequiredSection("DateFormat").Value ?? "";
                var file = _reader.Read(fileName);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new CustomDateTimeConverter(dateFormat) }
                };

                var data = JsonSerializer.Deserialize<IEnumerable<T>>(file, options);
                return data ?? Enumerable.Empty<T>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
                return Enumerable.Empty<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return Enumerable.Empty<T>();
            }
        }
    }
}
