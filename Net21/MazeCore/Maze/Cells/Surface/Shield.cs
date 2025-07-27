using MazeCore.Maze.Cells.Characters;

namespace MazeCore.Maze.Cells.Surface
{
    public class Shield : BaseCell
    {
        /// <summary>
        /// Shield add +2 HP for user
        /// </summary>

        public Shield(int x, int y, IMazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "/";

        public override bool TryStep(IBaseCharacter hero)
        {
            hero.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}

