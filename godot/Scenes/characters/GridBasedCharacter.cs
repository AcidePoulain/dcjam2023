using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public abstract partial class GridBasedCharacter : CharacterBody3D
{
	public RayCast3D FrontRayCast { get; private set; }
	public RayCast3D BackRayCast { get; private set; }
	public RayCast3D LeftRayCast { get; private set; }
	public RayCast3D RightRayCast { get; private set; }

	public override void _Ready()
	{
		base._Ready();

		FrontRayCast = GetNode<RayCast3D>("FrontCollisionRaycast");
		BackRayCast = GetNode<RayCast3D>("BackCollisionRaycast");
		LeftRayCast = GetNode<RayCast3D>("LeftCollisionRaycast");
		RightRayCast = GetNode<RayCast3D>("RightCollisionRaycast");
	}

	public Vector2I GridPos()
	{
		var gridPos = (Position / 2).Round();
		return new Vector2I((int)gridPos.X, (int)gridPos.Z);
	}
}
