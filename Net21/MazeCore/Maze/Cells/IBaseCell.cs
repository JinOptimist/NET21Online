using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells
{
    public interface IBaseCell
    {
        IMazeMap MazeMap { get; set; }
        string Symbol { get; }
        int X { get; set; }
        int Y { get; set; }

        bool TryStep(IBaseCharacter character);
    }
}