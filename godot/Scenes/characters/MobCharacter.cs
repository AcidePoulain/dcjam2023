using System.Collections.Generic;
using DungeonCrawlerJam2023.Scenes.characters.ai;

namespace DungeonCrawlerJam2023.Scenes.characters;

public partial class MobCharacter : GridBasedCharacter
{
    protected IList<IAiTrait> Traits { get; } = new List<IAiTrait>();

    public override void _Ready()
    {
        base._Ready();

        TurnManager.NextTurn += OnNextTurn;
    }

    private void OnNextTurn(int currentTurn)
    {
        foreach (var trait in Traits)
            trait.Process(this, currentTurn);
    }
}
