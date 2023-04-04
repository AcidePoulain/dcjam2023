using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Player : GridBasedCharacter
{
	[Export]
	public float TranslationDuration = 0.2F;
	[Export]
	public float RotationDuration = 0.3F;

	private Tween? _tween;

	public override void _Input(InputEvent @event)
	{
		// This condition prevents us from processing input if tweening is in progress.
		// Be aware that if you animate something else than a player movement, you should not
		// use a tweener because it will block Player from reacting to input.
		if (_tween != null && _tween.IsRunning()) return;

		if (@event.IsActionPressed("forward"))
		{
			if (FrontRayCast.IsColliding())
			{
				InstantiateTween()
					.TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(0, 0, -2)).Origin,
						TranslationDuration);
			}
		}
		else if (@event.IsActionPressed("backward"))
		{
			if (BackRayCast.IsColliding())
			{
				InstantiateTween()
					.TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(0, 0, 2)).Origin,
						TranslationDuration);
			}
		}
		else if (@event.IsActionPressed("left"))
		{
			if (LeftRayCast.IsColliding())
			{
				InstantiateTween()
					.TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(-2, 0, 0)).Origin,
						TranslationDuration);
			}
		}
		else if (@event.IsActionPressed("right"))
		{
			if (RightRayCast.IsColliding())
			{
				InstantiateTween()
					.TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(2, 0, 0)).Origin,
						TranslationDuration);
			}
		}
		else if (@event.IsActionPressed("rotate_left"))
		{
			var rotation = RotationDegrees;
			rotation.Y += 90;
			InstantiateTween().TweenProperty(this, "rotation_degrees", rotation, RotationDuration);
		}
		else if (@event.IsActionPressed("rotate_right"))
		{
			var rotation = RotationDegrees;
			rotation.Y -= 90;
			InstantiateTween().TweenProperty(this, "rotation_degrees", rotation, RotationDuration);
		}
	}

	private Tween InstantiateTween()
	{
		return _tween = GetTree().CreateTween();
	}
}
