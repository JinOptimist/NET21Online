using MazeConsole.Builder;
using MazeConsole.Draw;


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

            var maze = builder.BuildSurface(42, 28);

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
                    case ConsoleKey.Tab:
                    case ConsoleKey.I:
                        hero.ManageInventory();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        isGameOver = true;
                        break;
                }

                var cell = maze[destinationX, destinationY];

                if (cell.TryStep(hero))
                {
                    hero.PastX = hero.X;
                    hero.PastY = hero.Y;
                    hero.X = destinationX;
                    hero.Y = destinationY;
                }

            } while (!isGameOver);

            
        }
    }
}
