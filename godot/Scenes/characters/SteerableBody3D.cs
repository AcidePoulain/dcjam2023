using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public abstract partial class SteerableBody3D : CharacterBody3D
{
    private Node3D? _pivot;

    private Vector3 _targetPosition = Vector3.Inf;

    [ExportCategory("SteerableBody3D")]
    [Export]
    public float Speed { get; set; } = 1F;

    [Export] public double DriftToleranceSquared { get; set; } = 0.015625;

    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set
        {
            _targetPosition = value;
            var direction = Position.DirectionTo(_targetPosition);
            _pivot?.LookAt(_targetPosition + Vector3.Up, Vector3.Up);
            Velocity = direction * Speed;
        }
    }

    public override void _Ready()
    {
        base._Ready();

        var pivot = GetNodeOrNull<Node3D>("Pivot");
        if (pivot != null)
            _pivot = pivot;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (TargetPosition == Vector3.Inf)
        {
            Velocity = Vector3.Zero;
            return;
        }

        if ((Position - TargetPosition).LengthSquared() <= DriftToleranceSquared * DriftToleranceSquared)
        {
            Velocity = Vector3.Zero;
            return;
        }

        MoveAndSlide();
    }
}
