using CommonModule.DataProviders.Helpers.Location;
using CommonModule.DataProviders.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.DataProviders.Helpers.DependencyInjection.Extensions
{
    public static class DataProviderExtensions
    {
        public static IServiceCollection AddDataProviders(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IDataObjectLocationResolver, DataObjectLocationResolver>()
                .AddTransient<IJsonDataProvider, JsonDataProvider>();
        }
    }
}
