using MazeConsole.Maze.Cells.Items;

namespace MazeConsole.Maze.Cells.Characters
{
    public interface IHero : IBaseCharacter
    {
        List<BaseItems> Inventory { get; set; }
        int SizeInventory { get; set; }
        string Symbol { get; }

        bool CanGet();
        List<string> GetInventoryNames();
        bool TryStep(IBaseCharacter character);
    }
}