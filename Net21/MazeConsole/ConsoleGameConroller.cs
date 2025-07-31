using MazeConsole.Draw;
using MazeCore;
using MazeCore.Builder;
using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Builder.LabirintBuilder;

namespace MazeConsole
{
    public class ConsoleGameConroller
    {
        /// <summary>
        /// Read key which user press to move hero
        /// </summary>
        private void Play()
        {
            var builder = new MazeBuilder();
            var drawer = new Drawer();

            var maze = builder.BuildSurface(30, 12);

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

        /// <summary>
        /// This method is for the game if the number of players is more than 1
        /// </summary>
        /// <param name="countPlayer"></param>
        private void PlayMultiplayer(int countPlayer)
        {
            var builder = new MazeBuilder();
            var drawer = new Drawer();

            var maze = builder.BuildSurface(30, 12, null, true, countPlayer); 

            var gameController = new GameConroller();

            do
            {
                foreach (var hero in maze.Heroes)
                {
                    drawer.DarwMultiplayer(maze, hero);

                    if (hero.Hp <= 0)
                    {
                        continue;
                    }

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
                        default:
                            // do nothing
                            continue;
                    }

                    gameController.OneTurnHero(maze, hero, direction);
                }

                gameController.OneTurnNpc(maze);

            } while (maze.Heroes.Any(c => c.isAlife));

            drawer.GameOver(maze);

        }

        public void SwitchTypeOfGame()
        {
            Console.WriteLine("Select game mode:");
            Console.WriteLine("1 - Single");
            Console.WriteLine("2 - Multiplayer");

            Console.Write("Enter your select (1 or 2): ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Play();
                    break;
                case "2":
                    SwitchCountPlayer();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong select! Try again.");
                    SwitchTypeOfGame();
                    return;
            }
        }

        private void SwitchCountPlayer()
        {
            Console.Write("Enter the number of players: ");
            var input = Console.ReadLine();

            if(!int.TryParse(input, out var count))
            {
                Console.Clear();
                Console.WriteLine("This is not a number! Try again.");
                SwitchCountPlayer();
            }

            PlayMultiplayer(count);
        }
    }
}
