namespace MazeConsole.Maze.Cells
{
    public class Wall : BaseCell
    {
        public Wall(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "#";
        
        public override bool TryStep(Hero hero)
        {
            return false;
        }
    }
}
