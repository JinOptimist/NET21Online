namespace MazeConsole.Maze.Cells;

public class Snake : BaseCell
{
    public Snake(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
    }
    
    public override string Symbol => "S";
    
    public override bool TryStep(Hero hero)
    {
        hero.Hp--;
        return true;
    }
}