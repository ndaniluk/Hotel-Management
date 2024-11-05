using Microsoft.Extensions.Configuration;
using Helpers.FileOperations;
using System.Text.Json;
using Helpers.Converters;

namespace Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private readonly IConfiguration _configuration;
        private readonly IFileReader _reader;

        public BaseRepository(IConfiguration configuration, IFileReader reader)
        {
            _configuration = configuration;
            _reader = reader;
        }

        public abstract T? GetById(string id);
        public abstract IEnumerable<T> GetAll();

        public IEnumerable<T> GetAllFromFile()
        {
            var fileName = _configuration.GetSection("Repositories").GetSection(typeof(T).Name).Value;
            if (string.IsNullOrEmpty(fileName))
            {
                return Enumerable.Empty<T>();
            }

            try
            {
                var file = _reader.Read(fileName);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new CustomDateTimeConverter("yyyyMMdd") }
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
