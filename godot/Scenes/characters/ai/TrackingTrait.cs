using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public class TrackingTrait : IAiTrait
{
    private readonly int _sightRangeSquared;
    private readonly int _trackingExpirationInTurns;

    private int _targetLastSeenTurn;

    public TrackingTrait(GridBasedCharacter target, int trackingExpirationInTurns, int sightRange)
    {
        Target = target;
        _trackingExpirationInTurns = trackingExpirationInTurns;
        _sightRangeSquared = sightRange * sightRange;
    }

    public bool CanSeeTarget { get; private set; }
    public bool RememberTarget { get; private set; }

    public GridBasedCharacter Target { get; }

    public void Process(GridBasedCharacter self, int currentTurn)
    {
        var gridPos = self.GridPos;
        var targetGridPos = Target.GridPos;

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
                _targetLastSeenTurn = currentTurn;
            }
        }
        else
        {
            CanSeeTarget = false;
        }

        RememberTarget = currentTurn - _targetLastSeenTurn > _trackingExpirationInTurns;
    }
}
