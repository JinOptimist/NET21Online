namespace MazeConsole.Maze.Cells;

public class InnerWall: Wall
{
    public InnerWall(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
    {
        mazeMap.ReplaceCell(this);
    }
}