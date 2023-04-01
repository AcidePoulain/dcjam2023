using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= _gravity * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("forward"))
		{
			var frontRayCast = GetNode<RayCast3D>("FrontCollisionRaycast");

			if (!frontRayCast.IsColliding())
			{
				var currentPosition = Position;
				currentPosition.Z -= 2;
				Position = currentPosition;
			}
		}
		else if (@event.IsActionPressed("backward"))
		{
			var backRayCast = GetNode<RayCast3D>("BackCollisionRaycast");

			if (!backRayCast.IsColliding())
			{
				var currentPosition = Position;
				currentPosition.Z += 2;
				Position = currentPosition;
			}
		}
		else if (@event.IsActionPressed("rotate_left"))
		{
			// rotate to the left
		}
		else if (@event.IsActionPressed("rotate_right"))
		{
			// rotate to the right
		}
	}
}
