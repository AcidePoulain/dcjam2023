namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public interface IAiTrait
{
    void Process(GridBasedCharacter self, int currentTurn);
}
