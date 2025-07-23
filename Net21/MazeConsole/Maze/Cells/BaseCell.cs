using MazeConsole.Maze.Cells.Characters;

namespace MazeConsole.Maze.Cells
{
    public abstract class BaseCell : IBaseCell
    {
        public BaseCell(int x, int y, IMazeMap mazeMap)
        {
            X = x;
            Y = y;
            MazeMap = mazeMap;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public IMazeMap MazeMap { get; set; }
        public abstract string Symbol { get; }

        public abstract bool TryStep(IBaseCharacter character);
    }
}
