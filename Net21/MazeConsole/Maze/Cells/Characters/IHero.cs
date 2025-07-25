using MazeConsole.Maze.Cells.Inventory;

namespace MazeConsole.Maze.Cells.Сharacters
{
    public interface IHero : IBaseCharacter
    {
        List<IBaseItems> Inventory { get; set; }
        int SizeInventory { get; set; }
        string Symbol { get; }

        bool CanGet();
        List<string> GetInventoryNames();
        bool TryStep(IBaseCharacter character);
    }
}