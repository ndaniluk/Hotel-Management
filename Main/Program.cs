using Main;
using Main.Extensions;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddServices()
    .AddRepositories()
    .AddCommands()
    .AddSingleton<App>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<App>();
app?.Start();