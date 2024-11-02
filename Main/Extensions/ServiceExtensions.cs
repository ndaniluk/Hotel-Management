using Microsoft.Extensions.DependencyInjection;
using Services.Availability;
using Services.Search;

namespace Main.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAvailabilityService, AvailabilityService>()
                .AddScoped<ISearchService, SearchService>();
        }
    }
}
