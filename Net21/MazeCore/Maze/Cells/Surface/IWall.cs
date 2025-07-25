using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

public interface IWall : IBaseCell
{
    string Symbol { get; }
    bool TryStep(IBaseCharacter hero);
}