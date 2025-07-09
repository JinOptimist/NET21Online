namespace MazeConsole.Maze.Cells
{
    public class Hero : BaseCell
    {
        public int Money { get; set; }
        public int Hp { get; set; }

        public Hero(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "@";

        public override bool TryStep(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
