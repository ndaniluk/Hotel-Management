using Microsoft.Extensions.DependencyInjection;
using CommonModule.Commands;
using CommonModule.DataProviders;

namespace CommonModule
{
    public static class CommonModule
    {
        public static IServiceCollection AddCommonModules(this IServiceCollection servicesCollection)
        {
            return servicesCollection
                .AddCommonModuleCommands()
                .AddCommonModuleDataProviders();
        }
    }
}
