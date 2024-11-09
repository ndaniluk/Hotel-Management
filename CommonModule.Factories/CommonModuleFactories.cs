using CommonModule.Factories.Helpers.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Factories
{
    public static class CommonModuleFactories
    {
        public static IServiceCollection AddCommonModuleFactories(this IServiceCollection servicesCollection)
        {
            return servicesCollection
                .AddCommands();
        }
    }
}