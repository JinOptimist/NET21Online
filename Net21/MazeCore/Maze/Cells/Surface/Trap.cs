using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface;

public class Trap : BaseCell
{
    public Trap(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
    }

    public override string Symbol => "*";

    public override bool TryStep(IBaseCharacter hero)
    {
        hero.Hp -= 2;
        var ground = new Ground(X, Y, MazeMap);
        MazeMap.ReplaceCell(ground);
        return true;
    }
}