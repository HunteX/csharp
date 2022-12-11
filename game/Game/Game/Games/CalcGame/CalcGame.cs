using Game.Configuration;

namespace Game.Games.CalcGame;

public sealed class CalcGame : Base.Game
{
    public CalcGame(GameConfigLoader gameConfigLoader) : base(gameConfigLoader)
    {
    }

    protected override void Run()
    {
        var config = (CalcGameConfig)GameConfig;

        Console.WriteLine(config.Question);

        var result = Console.ReadLine();

        var message = result == config.Answer
            ? "Поздравляю ʕ ᵔᴥᵔ ʔ Вы победили!"
            : "Вы проиграли ¯\\_(ツ)_/¯";

        Console.WriteLine(message);
    }
}