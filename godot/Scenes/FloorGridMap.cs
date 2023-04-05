using System.Linq;
using Godot;

namespace DungeonCrawlerJam2023.Scenes;

public partial class FloorGridMap : GridMap
{
    [Export] public int[] WalkableCellTypes = { 1 };
    [Export] public GridFather? GridFather;

    public override void _Ready()
    {
        base._Ready();

        if (GridFather == null)
            return;
        
        var cells = GetUsedCells()
            .Where(cellPos => WalkableCellTypes.Contains(GetCellItem(cellPos)))
            .Select(cell3D => new Vector2I(cell3D.X, cell3D.Z));

        GridFather.Grid = cells.ToArray();
    }
}
