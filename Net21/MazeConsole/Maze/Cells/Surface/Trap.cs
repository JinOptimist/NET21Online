using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Surface;

public class Trap : BaseCell
{
    public Trap(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
    }

    public override string Symbol => "*";

    public override bool TryStep(BaseCharacter hero)
    {
        hero.Hp -= 2;
        var ground = new Ground(X, Y, MazeMap);
        MazeMap.ReplaceCell(ground);
        return true;
    }
}