using MazeConsole.Maze.Cells.Inventory;


namespace MazeCore.Maze.Cells.Characters
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