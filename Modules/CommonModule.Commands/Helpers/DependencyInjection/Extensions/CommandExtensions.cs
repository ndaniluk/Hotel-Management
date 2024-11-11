using CommonModule.Commands.Composites;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Commands.Helpers.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICompositeCommandFactory, CompositeCommandFactory>()
                .AddSingleton<ICommandInvoker, CommandInvoker>();
        }
    }
}
