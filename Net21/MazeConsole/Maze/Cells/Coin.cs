namespace MazeConsole.Maze.Cells
{
    public class Coin : BaseCell
    {
        public Coin(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            mazeMap.ReplaceCell(this);
        }

        public override string Symbol => "c";

        public override bool TryStep(Hero hero)
        {
            hero.Money++;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
