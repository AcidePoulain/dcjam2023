using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public class FollowTrait : IAITrait
{
    private readonly TrackingTrait _tracker;

    public FollowTrait(TrackingTrait tracker)
    {
        _tracker = tracker;
    }

    public void Process(GridBasedCharacter self, double delta)
    {
        var gridPos = self.GridPos();
        var targetGridPos = _tracker.Target.GridPos();

        // TODO
    }

    private void Move(Vector2I destination)
    {
        // TODO
    }

    private Vector2I GetNextNodeAStar(Vector2I start, Vector2I end, params Vector2I[] nodes)
    {
        // TODO
        return start;
    }
}
