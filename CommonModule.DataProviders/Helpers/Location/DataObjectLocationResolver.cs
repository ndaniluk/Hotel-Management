using Microsoft.Extensions.Configuration;

namespace CommonModule.DataProviders.Helpers.Location
{
    public class DataObjectLocationResolver(IConfiguration configuration) : IDataObjectLocationResolver
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetLocation<T>()
        {
            var dataObjectLocation = _configuration[$"{typeof(T).Name.ToLower()}s"];

            if (string.IsNullOrEmpty(dataObjectLocation))
            {
                Console.WriteLine("Couldn't resolve data object name.");
                return string.Empty;
            }

            return dataObjectLocation;
        }
    }
}
