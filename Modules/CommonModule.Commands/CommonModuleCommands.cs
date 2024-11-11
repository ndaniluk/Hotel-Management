using CommonModule.Commands.Helpers.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Commands
{
    public static class CommonModuleCommands
    {
        public static IServiceCollection AddCommonModuleCommands(this IServiceCollection servicesCollection)
        {
            return servicesCollection
                .AddCommands();
        }
    }
}
