using MazeSmile.MazeData.Cells;

namespace MazeSmile
{
    public class Maze
    {
        public int Width { get; }
        public int Height { get; }
        public BaseCell[,] Cells { get; }

        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            Cells = MazeGenerator.Generate(width, height);
        }

        public bool IsInside(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    }
}
