using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Player : GridBasedCharacter
{
    private Tween? _tween;
    [Export] public float RotationDuration = 0.3F;
    [Export] public float TranslationDuration = 0.2F;


    public override void _Input(InputEvent @event)
    {
        // This condition prevents us from processing input if tweening is in progress.
        // Be aware that if you animate something else than a player movement, you should not
        // use a tweener because it will block Player from reacting to input.
        if (_tween != null && _tween.IsRunning()) return;

        var walkingSound = GetNode<AudioStreamPlayer3D>("WalkSound");

        if (@event.IsActionPressed("forward"))
        {
            var nextPosition = Transform.TranslatedLocal(new Vector3(0, 0, -2)).Origin;
            walkingSound.Play();
            MoveToCell(nextPosition);
        }
        else if (@event.IsActionPressed("backward"))
        {
            var nextPosition = Transform.TranslatedLocal(new Vector3(0, 0, 2)).Origin;
            walkingSound.Play();
            MoveToCell(nextPosition);
        }
        else if (@event.IsActionPressed("left"))
        {
            var nextPosition = Transform.TranslatedLocal(new Vector3(-2, 0, 0)).Origin;
            walkingSound.Play();
            MoveToCell(nextPosition);
        }
        else if (@event.IsActionPressed("right"))
        {
            var nextPosition = Transform.TranslatedLocal(new Vector3(2, 0, 0)).Origin;
            walkingSound.Play();
            MoveToCell(nextPosition);
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

    private void MoveToCell(Vector3 nextPosition)
    {
        var nextCell = SnapPositionToGrid(nextPosition);

        if (!Father.IsCellValid(nextCell) || !Father.IsCellFree(nextCell)) return;

        InstantiateTween()
            .TweenProperty(this, "position", nextPosition, TranslationDuration);
        Father.MoveToCell(this, nextCell);
        TurnManager.DoNextTurn();
    }

    private Tween InstantiateTween()
    {
        return _tween = GetTree().CreateTween();
    }
}
