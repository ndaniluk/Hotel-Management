using Helpers.FileOperations;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Extensions
{
    public static class HelpersExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            return services
                .AddSingleton<IFileReader, FileReader>();
        }
    }
}
