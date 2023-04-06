using System.Collections.Generic;
using Godot;
using DungeonCrawlerJam2023.Scenes.characters.ai;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Ticketless : GridBasedCharacter
{
    [Signal]
    public delegate void TookDamageEventHandler(float amount);
    [Signal]
    public delegate void HostileDiedEventHandler();


    [Export] public GridBasedCharacter Target;
    [Export] public ulong TargetMemoryMS = 10_000;
    [Export] public int SightRange = 20;

    [Export] private float _healthPoints = 10.0f;

    private readonly IList<IAITrait> _traits = new List<IAITrait>();

    public override void _Ready()
    {
        base._Ready();

        var trackingTrait = new TrackingTrait(Target, TargetMemoryMS, SightRange);
        _traits.Add(trackingTrait);
        _traits.Add(new FollowTrait(trackingTrait));
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        foreach (var trait in _traits)
            trait.Process(this, delta);
    }

    public void TakeDamage(float amount) {
        _healthPoints -= amount;
        EmitSignal(SignalName.TookDamage, amount);

        if (_healthPoints <= 0.0f) {
            EmitSignal(SignalName.HostileDied);
            QueueFree();
        }
    }
}
