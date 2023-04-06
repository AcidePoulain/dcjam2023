using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public class FollowTrait : IAiTrait
{
    private readonly TrackingTrait _tracker;

    public FollowTrait(TrackingTrait tracker)
    {
        _tracker = tracker;
    }

    public void Process(GridBasedCharacter self, int currentTurn)
    {
        if (_tracker is { CanSeeTarget: false, RememberTarget: false }) return;
        GD.Print("Following target");

        var gridPos = self.GridPos;
        var targetGridPos = _tracker.Target.GridPos;

        var nextPosition = GetNextNodeAStar(gridPos, targetGridPos, self.Father.Grid);

        if (nextPosition == gridPos) return;

        Move(self, nextPosition);
        GD.Print($"Moving to {nextPosition}");
    }

    private void Move(GridBasedCharacter self, Vector2I destination)
    {
        self.Father.MoveToCell(self, destination);
    }

    private Vector2I GetNextNodeAStar(Vector2I start, Vector2I end, params Vector2I[] nodes)
    {
        var closed = new List<Vector2I>();
        var open = new PriorityQueue<Vector2I, int>();
        open.Enqueue(start, 0);

        // cheating a bit - the cost will always be 1 more than the previous position.
        // all tiles have the same cost. So i simply increment the cost every loop.
        var cost = 0;
        while (open.Count > 0)
        {
            var currentTile = open.Dequeue();
            if (currentTile.X == end.X && currentTile.Y == end.Y) return closed[1];

            var neighbors = new Vector2I[]
            {
                new(currentTile.X, currentTile.Y + 1),
                new(currentTile.X, currentTile.Y - 1),
                new(currentTile.X + 1, currentTile.Y),
                new(currentTile.X - 1, currentTile.Y)
            };

            foreach (var neighbor in neighbors)
            {
                // These gymnastics are needed because
                // I can not check if a position is in our openlist.
                // This is because open is of type PriorityQueue, which
                // does not allow the use of Contains() directly on it.
                var unorderedOpens = open.UnorderedItems.ToList()
                    .Select(_ => _.Item1)
                    .ToList();

                // check this is a walkable tile
                if (nodes.Contains(neighbor))
                    // check that we did not already walk on this
                    if (!(closed.Contains(neighbor) || unorderedOpens.Contains(neighbor)))
                    {
                        // Do not call Sqrt(). We do not need to know the "real" distance,
                        // knowing that a point will be farther or closer is enough.
                        var distance = (int)Mathf.Pow(neighbor.X - end.X, 2) + (int)Mathf.Pow(neighbor.Y - end.Y, 2);
                        open.Enqueue(neighbor, cost + distance);
                    }
            }

            closed.Add(currentTile);
            cost++;
        }

        // TODO this should not be reachable. We have to
        // raise an exception, or crash, or do something.
        return start;
    }
}
