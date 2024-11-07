using Main;
using Main.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string hotelsFile = configuration["hotels"];
string bookingsFile = configuration["bookings"];

if (string.IsNullOrEmpty(hotelsFile) || string.IsNullOrEmpty(bookingsFile))
{
    Console.WriteLine("Error: Missing data files for hotels or bookings.");
    return;
}   

var serviceProvider = new ServiceCollection()
    .AddServices()
    .AddRepositories()
    .AddCommands()
    .AddHelpers()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<App>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<App>();
app?.Start();