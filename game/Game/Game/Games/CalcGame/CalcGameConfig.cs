using Game.Interfaces;

namespace Game.Games.CalcGame;

public sealed class CalcGameConfig : IGameConfig
{
    public string Question { get; set; }
    public string Answer { get; set; }
}