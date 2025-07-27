using MazeConsole.Draw;
using MazeCore;
using MazeCore.Builder;
using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;

namespace MazeConsole
{
    public class ConsoleGameConroller
    {
        /// <summary>
        /// Read key which user press to move hero
        /// </summary>
        private MazeMap _currentMaze;
        private int _maxLevelAchieved = 1;

        public void Play()
        {
            var builder = new MazeBuilder();
            var drawer = new Drawer();

            _currentMaze = builder.BuildSurface(30, 12);
            LevelEvents(_currentMaze);
            var gameController = new GameConroller();

            var isAlive = false;
            do
            {
                drawer.Darw(_currentMaze);

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

                isAlive = gameController.OneTurn(_currentMaze, direction);
                if (!isAlive)
                {
                    Console.Clear();
                    Console.WriteLine($"You die. Your hp is {_currentMaze.Hero.Hp}. Your money is {_currentMaze.Hero.Money}");
                    Console.WriteLine($"Your max maze level achieved: {_maxLevelAchieved}");
                }
            } while (isAlive);
        }
        
        private void OnNextLevel()
        {
            if (_currentMaze.NextLevel == null)
            {
                var builder = new MazeBuilder();
                var newMaze = builder.BuildSurface();

                newMaze.PrevLevel = _currentMaze;
                _currentMaze.NextLevel = newMaze;
                newMaze.Level = _currentMaze.Level + 1;

                MoveHeroTo(newMaze, _currentMaze);
                LevelEvents(newMaze);
            }

            _currentMaze = _currentMaze.NextLevel;
            
            if (_currentMaze.Level > _maxLevelAchieved)
            {
                _maxLevelAchieved = _currentMaze.Level;
            }
        }

        private void OnPrevLevel()
        {
            if (_currentMaze.PrevLevel != null)
            {
                MoveHeroTo(_currentMaze.PrevLevel, _currentMaze);
                _currentMaze = _currentMaze.PrevLevel;
            }
        }
        
        private void MoveHeroTo(MazeMap to, MazeMap from)
        {
            var oldHero = from.Hero;

            var newHero = new Hero(1, 1, to)
            {
                Hp = oldHero.Hp,
                Money = oldHero.Money,
                SizeInventory = oldHero.SizeInventory
            };

            to.Hero = newHero;
        }
        
        private void LevelEvents(MazeMap maze)
        {
            maze.OnRequestPrevLevel = OnPrevLevel;
            maze.OnRequestNextLevel = OnNextLevel;
        }
    }
}
