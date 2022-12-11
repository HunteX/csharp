using Game.Interfaces;

namespace Game.Games.GuessingGame;

public sealed class GuessingGameConfig : IGameConfig
{
    public RandomRange RandomRange { get; set; }
    public int NumberOfAttempts { get; set; }
}

public sealed class RandomRange
{
    public int Min { get; set; }
    public int Max { get; set; }
}