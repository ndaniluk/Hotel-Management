using Commands.Availability;
using Factories.Commands;
using Commands.Search;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services
                .AddTransient<AvailabilityCommand>()
                .AddTransient<SearchCommand>()
                .AddSingleton<ICommandInvoker, CommandInvoker>()
                .AddSingleton<ICommandFactory, CommandFactory>();
        }
    }
}
