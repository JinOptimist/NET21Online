using MazeConsole.Builder;
using MazeConsole.Draw;
using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using System.Globalization;

namespace MazeConsole
{
    public class GameConroller
    {
        /// <summary>
        /// Read key which user press to move hero
        /// </summary>

        private MineManager _mineManager;
        public void Play()
        {
            var builder = new MazeBuilder();
            var drawer = new Drawer();

            var maze = builder.BuildSurface(12, 8);
            _mineManager = new MineManager(maze);
            var isGameOver = false;
            do
            {
                drawer.Darw(maze);

                _mineManager.Update();

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

                if (hero.Hp <= 0) 
                {
                    Console.Clear();
                    Console.WriteLine("GAME OVER!! You've hit a landmine!");
                    isGameOver = true;
                }


            } while (!isGameOver);

            
        }
    }
}
