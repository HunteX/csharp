using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Game.Configuration;
using Game.UI;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, configuration) => configuration.AddJsonFile("appsettings.json"))
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection.AddSingleton<GameConfigLoader>();
        serviceCollection.AddSingleton<GameSelector>();

        var gameTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Game.Games.Base.Game)))
            .ToList();

        gameTypes.ForEach(gameType => serviceCollection.AddSingleton(gameType));
    })
    .Build();

var gamesLoader = host.Services.GetService<GameSelector>()!;

gamesLoader.ShowGameListMenu();