using MazeConsole.Maze;
using MazeConsole.Maze.Cells;
using System.Security.Cryptography;

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
            BuildFirstAidKit();

            BuildHero();

            return _currentSurface;
        }
        
        private void BuildFirstAidKit()
        {
            var cellToReplace = _currentSurface.CellsSurface.OfType<Ground>().Where(cell => cell.X != 1 && cell.Y != 1).ToList();
            var numberСellsFirstAidKit = (int)(cellToReplace.Count * 0.05);
            var random = new Random();
            for (int i = 0; i < numberСellsFirstAidKit; i++) 
            {
                cellToReplace = _currentSurface.CellsSurface.OfType<Ground>().ToList();
                var randomIndex = random.Next(cellToReplace.Count);
                var randomCell = cellToReplace[randomIndex];
                var firstAidKit = new FirstAidKit(randomCell.X, randomCell.Y, _currentSurface);
                _currentSurface.ReplaceCell(firstAidKit);
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
