using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public class TrackingTrait : IAITrait
{
    public bool CanSeeTarget { get; private set; }
    public bool RememberTarget { get; private set; }

    public GridBasedCharacter Target { get; }
    private readonly ulong _trackingExpirationInMs;
    private readonly int _sightRangeSquared;

    private ulong _targetLastSeenInMs;

    public TrackingTrait(GridBasedCharacter target, ulong trackingExpirationInMs, int sightRange)
    {
        Target = target;
        _trackingExpirationInMs = trackingExpirationInMs;
        _sightRangeSquared = sightRange * sightRange;
    }

    public void Process(GridBasedCharacter self, double delta)
    {
        var currentTime = Time.GetTicksMsec();
        var gridPos = self.GridPos();
        var targetGridPos = Target.GridPos();

        // Sharing an axis
        if ((gridPos - targetGridPos).Abs().LengthSquared() <= _sightRangeSquared)
        {
            var spaceState = self.GetWorld3D().DirectSpaceState;
            // use global coordinates, not local to node
            var query = PhysicsRayQueryParameters3D.Create(self.Position, Target.Position);
            var result = spaceState.IntersectRay(query);

            // Line of sight
            if (result.Count != 0)
            {
                CanSeeTarget = true;
                _targetLastSeenInMs = currentTime;
            }
        }
        else
            CanSeeTarget = false;

        RememberTarget = currentTime - _targetLastSeenInMs > _trackingExpirationInMs;
    }
}
