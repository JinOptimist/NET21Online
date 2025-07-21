using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;

public interface IGround : IBaseCell
{
    string Symbol { get; }
    bool TryStep(IBaseCharacter hero);
}