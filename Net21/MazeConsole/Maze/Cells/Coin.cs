using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y, MazeMap mazeMap, int countCoins = 1) : base(x, y, mazeMap)
        {
            _countCoins = countCoins;
        }

        public override string Symbol => "c";

        private int _countCoins;

        public override bool TryStep(BaseCharacter hero)
        {
            hero.Money += _countCoins;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
