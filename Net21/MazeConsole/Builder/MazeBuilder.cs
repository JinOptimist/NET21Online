using MazeConsole.Maze;
using MazeConsole.Maze.Cells;

namespace MazeConsole.Builder
{
    public class MazeBuilder
    {
        private MazeMap _currentSurface;

        public MazeMap BuildSurface(int width, int height)
        {
            _currentSurface = new MazeMap(width, height);

            BuildWall();
            BuildGround();
            BuildCoin();


            BuildHero();
            BuildShield();
            return _currentSurface;
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
            var groundCell = _currentSurface.CellsSurface.OfType<Ground>()
              .Where(cell => (cell.X != _currentSurface.Hero.X)
              && _currentSurface.CellsSurface.OfType<Coin>()
              .Any(coin => coin.X != cell.X && coin.Y != cell.Y)
              && _currentSurface.CellsSurface.OfType<Wall>()
              .Any(wall => wall.X != cell.X && wall.Y != cell.Y))
              .ToList();
           
            var random = new Random();
            var randomCell = random.Next(groundCell.Count);
            var generateCoordinate = groundCell[randomCell];
            var X = generateCoordinate.X;
            var Y = generateCoordinate.Y;
            return (X, Y);

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
    }
}
