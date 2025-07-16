using MazeConsole.Maze.Cells.Сharacters;

namespace MazeConsole.Maze.Cells.Surface
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y, IMazeMap mazeMap, int countCoins = 1) : base(x, y, mazeMap)
        {
            if (countCoins < 0)
            {
                throw new ArgumentException("Bad dev");
            }

            _countCoins = countCoins;
        }

        public override string Symbol => "c";

        private int _countCoins;

        public override bool TryStep(IBaseCharacter hero)
        {
            hero.Money += _countCoins;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
