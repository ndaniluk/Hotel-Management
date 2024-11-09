using Main;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingModule;
using CommonModule;
public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddCommandLine(args)
            .Build();

        if (string.IsNullOrEmpty(configuration["hotels"]))
        {
            configuration["hotels"] = "hotels.json";
        }

        if (string.IsNullOrEmpty(configuration["bookings"]))
        {
            configuration["bookings"] = "bookings.json";
        }

        if (string.IsNullOrEmpty(configuration["dateFormat"]))
        {
            configuration["dateFormat"] = "yyyyMMdd";
        }

        var serviceProvider = new ServiceCollection()
            .AddBookingModule()
            .AddCommonModules()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<App>()
            .BuildServiceProvider();

        var app = serviceProvider.GetService<App>();
        app?.Start();
    }
}