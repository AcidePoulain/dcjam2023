﻿using System.Linq;
using DungeonCrawlerJam2023.Scenes.characters;
using Godot;
using Godot.Collections;

namespace DungeonCrawlerJam2023.Scenes;

public partial class GridFather : Node
{
    public Vector2I[] Grid { get; set; } = null!;

    private readonly Dictionary<Vector2I, GridBasedCharacter> _occupancyMap = new();

    private GridBasedCharacter[] _children = null!;

    public override void _Ready()
    {
        base._Ready();

        _children = GetChildren().Where(child => child is GridBasedCharacter)
            .Cast<GridBasedCharacter>()
            .ToArray();

        foreach (var character in _children)
            _occupancyMap[character.GridPos] = character;
    }

    public void MoveToCell(GridBasedCharacter character, Vector2I destination)
    {
        var previousCell = character.GridPos;
        _occupancyMap.Remove(previousCell);
        _occupancyMap[destination] = character;
    }

    public bool IsCellFree(Vector2I cell)
    {
        return !_occupancyMap.ContainsKey(cell);
    }

    public bool IsCellValid(Vector2I cell)
    {
        return Grid.Contains(cell);
    }
}