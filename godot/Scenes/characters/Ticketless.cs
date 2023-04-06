using DungeonCrawlerJam2023.Scenes.characters.ai;
using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class Ticketless : MobCharacter
{
    [Signal]
    public delegate void HostileDiedEventHandler();

    [Signal]
    public delegate void TookDamageEventHandler(float amount);


    [Export] private float _healthPoints = 10.0f;
    [Export] public int SightRange = 20;


    [Export] public GridBasedCharacter Target = null!;
    [Export] public int TargetMemoryTurns = 10;

    public override void _Ready()
    {
        base._Ready();

        var trackingTrait = new TrackingTrait(Target, TargetMemoryTurns, SightRange);
        Traits.Add(trackingTrait);
        Traits.Add(new FollowTrait(trackingTrait));
    }

    public void TakeDamage(float amount)
    {
        _healthPoints -= amount;
        EmitSignal(SignalName.TookDamage, amount);

        if (!(_healthPoints <= 0.0f)) return;

        EmitSignal(SignalName.HostileDied);
        QueueFree();
    }
}
