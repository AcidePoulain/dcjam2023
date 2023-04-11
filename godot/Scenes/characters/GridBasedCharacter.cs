using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public abstract partial class GridBasedCharacter : SteerableBody3D
{
    public GridFather Father { get; private set; } = null!;
    public TurnManager TurnManager { get; private set; } = null!;

    public Vector2I GridPos => SnapPositionToGrid(Position);

    public override void _Ready()
    {
        base._Ready();

        Father = GetParent<GridFather>();
        TurnManager = GetNode<TurnManager>("/root/Main/TurnManager");
    }

    public static Vector2I SnapPositionToGrid(Vector3 position)
    {
        var gridPos = (position / 2).Floor();
        return new Vector2I((int)gridPos.X, (int)gridPos.Z);
    }
}
