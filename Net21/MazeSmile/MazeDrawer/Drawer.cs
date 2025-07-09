using MazeSmile.MazeData.Cells;

namespace MazeSmile.MazeDrawer
{
    public static class Drawer
    {
        public static void DrawMaze(Maze maze, Player player)
        {
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    if (player.X == x && player.Y == y)
                    {
                        Console.Write('P');
                    }
                    else
                    {
                        var cell = maze.Cells[x, y];
                        Console.Write(cell switch
                        {
                            WallCell => '#',
                            GroundCell => '.',
                            _ => ' '
                        });
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
