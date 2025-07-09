namespace MazeConsole.Maze.Cells
{
    public abstract class BaseCell
    {
        public BaseCell(int x, int y, MazeMap mazeMap)
        {
            X = x;
            Y = y;
            MazeMap = mazeMap;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public MazeMap MazeMap { get; set; }
        public abstract string Symbol { get; }

        public abstract bool TryStep(Hero hero);
    }
}
