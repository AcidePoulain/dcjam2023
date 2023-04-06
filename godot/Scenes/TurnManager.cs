using Godot;

namespace DungeonCrawlerJam2023.Scenes;

public partial class TurnManager : Node
{
    [Signal]
    public delegate void NextTurnEventHandler(int currentTurn);

    public int CurrentTurn { get; private set; }

    public void DoNextTurn()
    {
        CurrentTurn++;
        GD.Print($"Turn {CurrentTurn}");

        EmitSignal(SignalName.NextTurn, CurrentTurn);
    }
}
