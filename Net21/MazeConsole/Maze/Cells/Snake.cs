using MazeConsole.Maze.Cells.Characters;

namespace MazeConsole.Maze.Cells;

public class Snake : BaseCell
{
    public Snake(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
    {
    }
    
    public override string Symbol => "S";
    
    public override bool TryStep(IBaseCharacter character)
    {
        character.Hp--;
        return true;
    }
}