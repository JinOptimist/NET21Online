using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters;

public interface IWall : IBaseCell
{
    string Symbol { get; }
    bool TryStep(IBaseCharacter hero);
}