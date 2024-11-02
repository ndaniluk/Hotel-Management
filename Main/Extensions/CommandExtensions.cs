using Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Extensions
{
    public static class CommandExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services
                .AddTransient<ICommand, AvailabilityCommand>()
                .AddTransient<ICommand, SearchCommand>();
        }
    }
}
