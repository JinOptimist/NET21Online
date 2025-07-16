using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells;

public class Lava : BaseCell
{
    public Lava(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
    }
    
    public override string Symbol => "№";

    public override bool TryStep(BaseCharacter hero)
    {
        hero.Hp = 0;
        hero.Money = 0;
        return false;
    }
}