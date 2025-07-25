using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

public interface IGround : IBaseCell
{
    string Symbol { get; }
    bool TryStep(IBaseCharacter hero);
}