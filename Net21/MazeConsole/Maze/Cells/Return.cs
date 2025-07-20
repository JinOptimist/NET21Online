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
            hero.X = 1;
            hero.Y = 1;
            return false;
        }
    }
}
