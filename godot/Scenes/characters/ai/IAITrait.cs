namespace DungeonCrawlerJam2023.Scenes.characters.ai;

public interface IAITrait
{
    void Process(GridBasedCharacter self, double delta);
}
