using CommonModule.Factories.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CommonModule.Factories.Helpers.DependencyInjection.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ICommandInvoker, CommandInvoker>()
                .AddSingleton<ICommandFactory, CommandFactory>();
        }
    }
}
