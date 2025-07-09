using MazeConsole.Maze.Cells;

namespace MazeConsole.Maze
{
    /// <summary>
    /// Buisness Model which store data about cells, hero and enemies
    /// </summary>
    public class MazeMap
    {
        public int Width { get; init; }
        public int Height { get; init; }
        
        public Hero Hero { get; set; }
        public List<BaseCell> CellsSurface { get; init; } = new List<BaseCell>();

        public MazeMap PrevLevel { get; set; }
        public MazeMap NextLevel { get; set; }

        public BaseCell this[int x, int y]
        {
            get
            {
                return CellsSurface
                    .First(cell => cell.X == x && cell.Y == y);
            }
        }

        public MazeMap(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void ReplaceCell(BaseCell newCell)
        {
            var oldCell = this[newCell.X, newCell.Y];
            CellsSurface.Remove(oldCell);
            CellsSurface.Add(newCell);
        }
    }
}
