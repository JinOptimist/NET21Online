using MazeConsole.Draw;
using MazeCore;
using MazeCore.Builder.LabirintBuilder;

namespace MazeConsole
{
    public class ConsoleGameConroller
    {
        /// <summary>
        /// Read key which user press to move hero
        /// </summary>
        public void Play()
        {
            var builder = new LabirintBuilder();
            var drawer = new Drawer();

            var maze = builder.BuildSurface(53, 27);

            var gameController = new GameConroller();

            var isAlive = false;
            do
            {
                drawer.Darw(maze);

                var key = Console.ReadKey();

                Direction direction = Direction.North;
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = Direction.North;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction = Direction.West;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = Direction.South;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction = Direction.East;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Q:
                        Console.WriteLine($"See you");
                        return;
                    default:
                        // do nothing
                        continue;
                }

                isAlive = gameController.OneTurn(maze, direction);
                if (!isAlive)
                {
                    Console.Clear();
                    Console.WriteLine($"You die. Your hp is {maze.Hero.Hp}. Your money is {maze.Hero.Money}");
                }
            } while (isAlive);
        }
    }
}
