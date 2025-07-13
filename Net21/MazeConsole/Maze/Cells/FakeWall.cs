namespace MazeConsole.Maze.Cells;


/// <summary>
/// Looks like a usual wall, but player can step on it
/// Usage: placed like a normal cell
/// </summary>
public class FakeWall: Wall
{
    public FakeWall(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }
    
    public override bool TryStep(Hero hero)
    {
        return true;
    }
}