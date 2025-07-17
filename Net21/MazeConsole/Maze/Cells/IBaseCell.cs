using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
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