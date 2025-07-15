using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "c";

        public override bool TryStep(BaseCharacter hero)
        {
            hero.Money++;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
