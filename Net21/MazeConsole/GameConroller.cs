using MazeConsole.Builder;
using MazeConsole.Draw;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using System.Globalization;

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

                maze.Npcs.ToList().ForEach(npc => CheckIsAlive(maze, npc));

                maze.Npcs.ForEach(TryMove);
                
                maze.ProcessNpcRequests();

                if (hero.Hp == 0)
                {
                    isGameOver = true;
                    Console.Clear();
                    Console.WriteLine($"You die. Your hp is {hero.Hp}. Your money is {hero.Money}");
                }

            } while (!isGameOver);
        }

        private void TryMove(BaseNpc npc)
        {
            var cell = npc.CellToMove();

            if (cell != null)
            {
                if (cell.TryStep(npc))
                {
                    npc.X = cell.X;
                    npc.Y = cell.Y;
                }
            }
        }

        private void CheckIsAlive(MazeMap maze, BaseNpc npc)
        {
            if (npc.Hp <= 0)
            {
                maze.Npcs.Remove(npc);
                var coin = new Coin(npc.X, npc.Y, maze);
                maze.ReplaceCell(coin);
            }
        }
    }
}
