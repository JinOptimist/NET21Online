using MazeSmile.MazeData.Cells;

namespace MazeSmile
{
    public class MazeGenerator
    {
        public static BaseCell[,] Generate(int width, int height)
        {
            var cells = new BaseCell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        cells[x, y] = new WallCell();
                    else
                        cells[x, y] = new GroundCell();
                }
            }
            return cells;
        }
    }
}
