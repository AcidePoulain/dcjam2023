using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public abstract partial class GridBasedCharacter : CharacterBody3D
{
    public GridFather? Father { get; private set; }

    public Vector2I GridPos => SnapPositionToGrid(Position);

    public override void _Ready()
    {
        base._Ready();

        Father = GetParent<GridFather>();
    }

    public static Vector2I SnapPositionToGrid(Vector3 position)
    {
        var gridPos = (position / 2).Floor();
        return new Vector2I((int)gridPos.X, (int)gridPos.Z);
    }
}
