namespace MazeConsole.Maze.Cells
{
    public class Return : BaseCell
    {
        public Return(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "<";

        public override bool TryStep(Hero hero)
        {
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);

            var heroNull = new Hero(1, 1, MazeMap);
            MazeMap.ReplaceCell(heroNull);
            return true;
        }
    }
}
