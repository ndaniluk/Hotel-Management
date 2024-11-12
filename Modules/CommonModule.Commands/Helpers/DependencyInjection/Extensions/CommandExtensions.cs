using CommonModule.Commands.Composites;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Commands.Helpers.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<ICompositeCommandFactory, CompositeCommandFactory>()
                .AddTransient<ICommandInvoker, CommandInvoker>();
        }
    }
}
