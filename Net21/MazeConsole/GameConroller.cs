using MazeConsole.Builder;
using MazeConsole.Draw;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters.Npcs;

namespace MazeConsole
{
    public class GameConroller
    {
        /// <summary>
        /// Read key which user press to move hero
        /// </summary>
        public void Play()
        {
            var builder = new MazeBuilder();
            var drawer = new Drawer();

            var maze = builder.BuildSurface(24, 8);

            var isGameOver = false;
            do
            {
                drawer.Darw(maze);

                var key = Console.ReadKey();

                var hero = maze.Hero;
                var destinationX = hero.X;
                var destinationY = hero.Y;

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        destinationY--;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        destinationX--;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        destinationY++;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        destinationX++;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        isGameOver = true;
                        break;
                    default:
                        // do nothing
                        break;
                }

                var cell = maze[destinationX, destinationY];

                if (cell.TryStep(hero))
                {
                    hero.X = destinationX;
                    hero.Y = destinationY;
                }

            } while (!isGameOver);           
        }

        private void CheckIsAlive(MazeMap maze, BaseNpc npc)
        {
            if (npc.Hp <= 0)
            {
                maze.Npcs.Remove(npc);
                var coin = new Coin(npc.X, npc.Y, maze, npc.Money);
                maze.ReplaceCell(coin);
            }
        }
    }
}
