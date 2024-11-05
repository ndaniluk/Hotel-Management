using Main;
using Main.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

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