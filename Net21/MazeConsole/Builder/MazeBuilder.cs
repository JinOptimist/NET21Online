using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using MazeConsole.Maze.Cells.Characters.Npcs;
using MazeConsole.Maze.Cells.Inventory;
using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using MazeConsole.Maze.Cells.Сharacters.Npcs;

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
            BuildReturn();
            BuildSnake(3);
            BuildTrap();
            BuildBoat();
            BuildTeleports();
            BuildIce();
            BuildShield();
            BuildHealingWell();
            BuildFirstAidKit();

            // Build npc
            BuildSnow();
            BuildGoblin();
            BuildThief();
            BuildDragon();
            BuildWizard();
            BuildWolf();
            BuildCultist();
            BuildSentry();
            // Build hero
            BuildHero();
            
            return _currentSurface;
        }

        private void BuildReturn()
        {
            var returN = new Return(6, 5, _currentSurface);
            _currentSurface.ReplaceCell(returN);
        }

        private void BuildCultist()
        {
            var ground = GetRandomGroundCell();
            var cultist = new Cultist(ground.X, ground.Y, _currentSurface);
            _currentSurface.Npcs.Add(cultist);
        }

        private void BuildDragon(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                var ground = GetRandomGroundCell();
                var dragon = new Dragon(ground.X, ground.Y, _currentSurface, hp: 3, money: 10);
                _currentSurface.Npcs.Add(dragon);
            }
        }


        private void BuildSnow(int count = 2)
        {
            var ground = GetRandomGroundCell();
            for (int i = 0; i < count; i++)
            {
                var snow = new Snow(ground.X, ground.Y, _currentSurface);
                _currentSurface.Npcs.Add(snow);
            }            
        }

        private void BuildSnake(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var ground = GetRandomGroundCell();
                var snake = new Snake(ground.X, ground.Y, _currentSurface);
                _currentSurface.ReplaceCell(snake);
            }
        }

        private void BuildWizard()
        {
            var ground = GetRandomGroundCell();
            var random = new Random();
            var isGoodMood = random.Next(0, 2) == 1 ? true : false;
            var wizard = new Wizard(ground.X, ground.Y, _currentSurface, isGoodMood);
            _currentSurface.Npcs.Add(wizard);
        }

        private void BuildFirstAidKit()
        {
            var cellsToReplace = _currentSurface.CellsSurface
                                 .OfType<Ground>()
                                 .Where(cell => cell.X != 1 && cell.Y != 1)
                                 .ToList();
            var difficultyFactor = 0.05;
            var numberCellsFirstAidKit = (int)(cellsToReplace.Count * difficultyFactor);

            for (int i = 0; i < numberCellsFirstAidKit; i++)
            {
                var randomCell = GetRandomGroundCell();
                var firstAidKit = new FirstAidKit(randomCell.X, randomCell.Y, _currentSurface);
                _currentSurface.ReplaceCell(firstAidKit);
            }

        }

        private void BuildShield()
        {
            var (x, y) = GetRandomCoordinateOfGround();
            var shield = new Shield(x, y, _currentSurface);
            _currentSurface.ReplaceCell(shield);
        }

        /// <summary>
        /// You can use this method after only BuildWall(); BuildCoin(); BuildHero() in BuildSurface();
        /// </summary>      
        public (int X, int Y) GetRandomCoordinateOfGround()
        {
            var groundCell = _currentSurface.CellsSurface.OfType<Ground>().ToList();
           
            var random = new Random();
            var randomCell = random.Next(groundCell.Count);
            var generateCoordinate = groundCell[randomCell];
            var x = generateCoordinate.X;
            var y = generateCoordinate.Y;
            return (x, y);

        }

        private void BuildGoblin(int count = 3)
        {
            var ground = GetRandomGroundCell();
            for (int i = 0; i < count; i++)
            {
                var goblin = new Goblin(ground.X, ground.Y, _currentSurface, 2, 1);
                _currentSurface.Npcs.Add(goblin);
            }
        }

        private void BuildThief()
        {
            var ground = GetRandomGroundCell();
            var thief = new Thief(ground.X, ground.Y, _currentSurface);
            _currentSurface.Npcs.Add(thief);
        }

        private void BuildWolf(int count = 2)
        {
            var ground = GetRandomGroundCell();
            for (int i = 0; i < count; i++)
            {
                var wolf = new Wolf(ground.X, ground.Y, _currentSurface, 2, 1);
                _currentSurface.Npcs.Add(wolf);
            }
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
            var boat = new Boat(3, 3, _currentSurface, "Boat");
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

        private void BuildTeleports()
        {
            var teleport1 = new Teleport(3, 2, _currentSurface);
            var teleport2 = new Teleport(6, 2, _currentSurface);
            teleport1.Bind(teleport2);
            teleport2.Bind(teleport1);
        }

        private void BuildIce()
        {
            for (int x = 3; x < 6; x++)
            {
                for (int y = 4; y < 7; y++)
                {
                    new Ice(x, y, _currentSurface);
                }
            }
        }

        private void BuildSentry()
        {
            var ground = GetRandomGroundCell();
            var sentry = new Sentry(ground.X, ground.Y, _currentSurface, 2, 1);   
            _currentSurface.Npcs.Add(sentry);
        }
    }
}
