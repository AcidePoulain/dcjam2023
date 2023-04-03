using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Player : CharacterBody3D
{
    // Used for movement animation
    private Tween _tween;
    public override void _Input(InputEvent @event)
    {
        if (_tween == null)
        {
            _tween = this.GetTree().CreateTween();
            _tween.Stop();
        }

        // This condition prevents us from processing input if tweening is in progress.
        // Be aware that if you animate something else than a player movement, you should not
        // use a tweener because it will block Player from reacting to input.
        if (!_tween.IsRunning())
        {
            if (@event.IsActionPressed("forward"))
            {
                var frontRayCast = GetNode<RayCast3D>("FrontCollisionRaycast");

                if (frontRayCast.IsColliding())
                {
                    this.Translate(new Vector3(0, 0, -2));
                }

                return;
            }
            else if (@event.IsActionPressed("backward"))
            {
                var backRayCast = GetNode<RayCast3D>("BackCollisionRaycast");

                if (backRayCast.IsColliding())
                {
                    this.Translate(new Vector3(0, 0, 2));
                }

                return;
            }
            else if (@event.IsActionPressed("left"))
            {
                var backRayCast = GetNode<RayCast3D>("LeftCollisionRaycast");

                if (backRayCast.IsColliding())
                {
                    this.Translate(new Vector3(-2, 0, 0));
                }

                return;
            }
            else if (@event.IsActionPressed("right"))
            {
                var backRayCast = GetNode<RayCast3D>("RightCollisionRaycast");

                if (backRayCast.IsColliding())
                {
                    this.Translate(new Vector3(2, 0, 0));
                }

                return;
            }
            else if (@event.IsActionPressed("rotate_left"))
            {
                _tween = this.GetTree().CreateTween();
                var rotation = this.RotationDegrees;
                rotation.Y += 90;
                _tween.TweenProperty(this, "rotation_degrees", rotation, 0.3f);

                return;
            }
            else if (@event.IsActionPressed("rotate_right"))
            {
                _tween = this.GetTree().CreateTween();
                var rotation = this.RotationDegrees;
                rotation.Y -= 90;
                _tween.TweenProperty(this, "rotation_degrees", rotation, 0.3f);

                return;
            }
        }
    }
}
