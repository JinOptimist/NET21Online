using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class Wall : BaseCell, IWall
    {
        public Wall(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "#";

        public override bool TryStep(IBaseCharacter hero)
        {
            if (hero is Hero h)
            {
            h.LogAction("You can't step on the wall");
            }
            return false;
        }
    }
}
