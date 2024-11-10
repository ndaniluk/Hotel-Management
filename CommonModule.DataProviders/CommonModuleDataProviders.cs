using CommonModule.DataProviders.Helpers.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.DataProviders
{
    public static class CommonModuleDataProviders
    {
        public static IServiceCollection AddCommonModuleDataProviders(this IServiceCollection servicesCollection)
        {
            return servicesCollection
                .AddDataProviders();
        }
    }
}
