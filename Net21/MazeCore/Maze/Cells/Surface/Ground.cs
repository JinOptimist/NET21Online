using MazeCore.Maze;
using MazeCore.Maze.Cells;
using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class Ground : BaseCell, IGround
    {
        public Ground(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => ".";

        public override bool TryStep(IBaseCharacter hero)
        {
            return true;
        }
    }
}