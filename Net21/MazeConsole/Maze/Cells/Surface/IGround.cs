using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters;

public interface IGround : IBaseCell
{
    string Symbol { get; }
    bool TryStep(IBaseCharacter hero);
}