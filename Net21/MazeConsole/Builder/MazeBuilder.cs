using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Inventory;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;
using System.Reflection;

namespace MazeConsole.Builder
{
    public class MazeBuilder
    {
        private MazeMap _currentSurface;
        private Random _random;

        public MazeMap BuildSurface(int width, int height, int? seed = null)
        {
            _random = new Random(seed ?? DateTime.Now.Microsecond);
            _currentSurface = new MazeMap(width, height);

            // Build surface
            BuildWall();
            BuildGround();
            BuildSea();
            BuildCoin();
            BuildTrap();
            BuildBoat();




            BuildHealingWell();

            // Build npc
            BuildGoblin();
            BuildThief();
            // Build hero
            BuildHero();

            return _currentSurface;
        }

        private void BuildGoblin(int count = 3)
        {
            var ground = GetRandomGroundCell();
            for (int i = 0; i < count; i++)
            {
                var goblin = new Goblin(ground.X, ground.Y, _currentSurface);
                _currentSurface.Npcs.Add(goblin);
            }
        }
        private void BuildThief()
        {
            var ground = GetRandomGroundCell();
            var thief = new Thief(ground.X, ground.Y, _currentSurface);
            _currentSurface.Npcs.Add(thief);
        }
        private void BuildCoin()
        {
            var cellToReplace = _currentSurface
                .CellsSurface
                .OfType<Ground>()
                .Where(cell => cell.X == _currentSurface.Width - 2
                    || cell.Y == _currentSurface.Height - 2)
                .ToList();
            foreach (var cell in cellToReplace)
            {
                var coin = new Coin(cell.X, cell.Y, _currentSurface);
                _currentSurface.ReplaceCell(coin);
            }
        }

        private void BuildBoat()
        {
            var boat = new Boat(3, 3, _currentSurface);
            _currentSurface.ReplaceCell(boat);
        }

        private void BuildHero()
        {
            var hero = new Hero(1, 1, _currentSurface);
            hero.Hp = 10;
            hero.Money = 3;
            _currentSurface.Hero = hero;
        }

        private void BuildGround()
        {
            var cellWhichWeReplaceToGround = _currentSurface
                .CellsSurface
                .Where(cell => cell.X != 0 && cell.Y != 0
                        && cell.X != _currentSurface.Width - 1
                        && cell.Y != _currentSurface.Height - 1)
                .ToList();

            foreach (var cell in cellWhichWeReplaceToGround)
            {
                var ground = new Ground(cell.X, cell.Y, _currentSurface);
                _currentSurface.ReplaceCell(ground);
            }
        }

        private void BuildSea()
        {
            foreach (var cell in _currentSurface
                .CellsSurface
                .Where(cell => cell.X > _currentSurface.Width / 2
                && cell.X != _currentSurface.Width - 1 && cell.Y != _currentSurface.Height - 1
                && cell.Y != 0 && cell.X != 0).ToList())
            {
                var sea = new Sea(cell.X, cell.Y, _currentSurface);
                _currentSurface.ReplaceCell(sea);
            }
        }

        private void BuildWall()
        {
            for (int y = 0; y < _currentSurface.Height; y++)
            {
                for (int x = 0; x < _currentSurface.Width; x++)
                {
                    var wall = new Wall(x, y, _currentSurface);
                    _currentSurface.CellsSurface.Add(wall);
                }
            }
        }

        private void BuildTrap()
        {
            var validCells = _currentSurface.CellsSurface
                .OfType<Ground>()
                .Where(cell => !(cell is Wall) && !(cell is Coin))
                .ToList();

            var random = new Random();
            var selectedCells = validCells.OrderBy(c => random.Next())
                .Take(5)
                .ToList();

            foreach (var cell in selectedCells)
            {
                var trap = new Trap(cell.X, cell.Y, _currentSurface);
                _currentSurface.ReplaceCell(trap);
            }
        }

        private void BuildHealingWell()
        {
            var ground = GetRandomGroundCell();

            var healingWell = new HealingWell(ground.X, ground.Y, _currentSurface);
            _currentSurface.ReplaceCell(healingWell);
        }

        private BaseCell GetRandomGroundCell()
        {
            var grounds = _currentSurface.CellsSurface
               .OfType<Ground>()
               .ToList();
            var index = _random.Next(grounds.Count);
            return grounds[index];
        }
    }
}
