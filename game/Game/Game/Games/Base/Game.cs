using Game.Configuration;
using Game.Interfaces;

namespace Game.Games.Base;

public abstract class Game
{
    protected IGameConfig GameConfig = null!;

    private readonly GameConfigLoader _gameConfigLoader;

    protected Game(GameConfigLoader gameConfigLoader)
    {
        _gameConfigLoader = gameConfigLoader;
    }

    public void Load()
    {
        var gameType = GetType();
        var configType = Type.GetType($"{gameType.FullName}Config");

        if (configType is null)
        {
            throw new Exception("Конфигурационный класс игры отсутствует!");
        }

        GameConfig = (IGameConfig)Activator.CreateInstance(configType)!;

        if (GameConfig is null)
        {
            throw new Exception("Конфигурационный файл игры отсутствует!");
        }

        _gameConfigLoader.Load($"Games:{gameType.Name}", ref GameConfig);

        Run();
    }

    protected virtual void Run()
    {
    }
}