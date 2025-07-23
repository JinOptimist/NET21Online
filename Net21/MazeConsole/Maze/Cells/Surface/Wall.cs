using MazeConsole.Maze.Cells.Characters;

namespace MazeConsole.Maze.Cells.Surface
{
    public class Wall : BaseCell, IWall
    {
        public Wall(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "#";

        public override bool TryStep(IBaseCharacter hero)
        {
            return false;
        }
    }
}
