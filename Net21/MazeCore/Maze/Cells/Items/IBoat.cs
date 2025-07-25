using MazeCore.Maze.Cells.Characters;

namespace MazeConsole.Maze.Cells.Inventory
{
    public interface IBoat: IBaseItems
    {
        string Symbol { get; }

        bool TryStep(IBaseCharacter character);
    }
}