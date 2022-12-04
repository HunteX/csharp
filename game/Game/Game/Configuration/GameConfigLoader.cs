using Game.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Game.Configuration;

public sealed class GameConfigLoader
{
    private readonly IConfiguration _configuration;

    public GameConfigLoader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Load(string sectionName, ref IGameConfig gameConfig)
    {
        _configuration.GetSection(sectionName).Bind(gameConfig);
    }
}