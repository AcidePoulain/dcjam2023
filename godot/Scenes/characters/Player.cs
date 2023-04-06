#nullable enable
using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Player : CharacterBody3D
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

        var walkingSound = GetNode<AudioStreamPlayer3D>("WalkSound");

        if (@event.IsActionPressed("forward"))
        {
            var frontRayCast = GetNode<RayCast3D>("FrontCollisionRaycast");

            if (frontRayCast.IsColliding())
            {
                walkingSound.Play();
                InstantiateTween()
                    .TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(0, 0, -2)).Origin,
                        TranslationDuration);
            }
        }
        else if (@event.IsActionPressed("backward"))
        {
            var backRayCast = GetNode<RayCast3D>("BackCollisionRaycast");

            if (backRayCast.IsColliding())
            {
                walkingSound.Play();
                InstantiateTween()
                    .TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(0, 0, 2)).Origin,
                        TranslationDuration);
            }
        }
        else if (@event.IsActionPressed("left"))
        {
            var backRayCast = GetNode<RayCast3D>("LeftCollisionRaycast");

            if (backRayCast.IsColliding())
            {
                walkingSound.Play();
                InstantiateTween()
                    .TweenProperty(this, "position", Transform.TranslatedLocal(new Vector3(-2, 0, 0)).Origin,
                        TranslationDuration);
            }
        }
        else if (@event.IsActionPressed("right"))
        {
            var backRayCast = GetNode<RayCast3D>("RightCollisionRaycast");

            if (backRayCast.IsColliding())
            {
                walkingSound.Play();
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
