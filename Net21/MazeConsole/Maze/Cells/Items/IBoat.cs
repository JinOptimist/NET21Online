using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Inventory
{
    public interface IBoat: IBaseItems
    {
        string Symbol { get; }

        bool TryStep(IBaseCharacter character);
    }
}