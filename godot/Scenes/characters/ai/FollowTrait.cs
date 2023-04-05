using System.Collections.Generic;
using System.Linq;

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
        var closed = new List<Vector2I>();
        var open = new PriorityQueue<Vector2I, int>();
        open.Enqueue(start, 0);

        // cheating a bit - the cost will always be 1 more than the previous position.
        // all tiles have the same cost. So i simply increment the cost every loop.
        int cost = 0;
        while (open.Count > 0)
        {
            var u = open.Dequeue();
            if (u.X == end.X && u.Y == end.Y)
            {
                return closed.First();
            }

            Vector2I[] possible_neighbors = new Vector2I[4]
            {
                new Vector2I(u.X, u.Y + 1),
                new Vector2I(u.X, u.Y - 1),
                new Vector2I(u.X + 1, u.Y),
                new Vector2I(u.X - 1, u.Y)
            };

            foreach (Vector2I neighbor in possible_neighbors)
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
                {
                    // check that we did not already walk on this
                    if (!(closed.Contains(neighbor) || (unorderedOpens.Contains(neighbor))))
                    {
                        // Do not call Sqrt(). We do not need to know the "real" distance,
                        // knowing that a point will be farther or closer is enough.
                        int distance = (int)Mathf.Pow((neighbor.X - end.X), 2) + (int)Mathf.Pow((neighbor.Y - end.Y), 2);
                        open.Enqueue(neighbor, cost + distance);
                    }
                }
            }

            closed.Add(u);
            cost++;
        };

        // TODO this should not be reachable. We have to
        // raise an exception, or crash, or do something.
        return start;
    }
}
