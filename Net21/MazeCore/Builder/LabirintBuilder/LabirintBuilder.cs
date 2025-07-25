using MazeCore.Maze;
using MazeCore.Maze.Cells.Characters;
using MazeCore.Maze.Cells.Surface;
using MazeCore.MazeExceptions;

namespace MazeCore.Builder.LabirintBuilder
{
    public class LabirintBuilder
    {
        private MazeMap _currentSurface;

        private int _seed;

        public LabirintBuilder(int? seed = null)
        {
            _seed = seed ?? DateTime.Now.Second;
        }

        public MazeMap BuildSurface(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new MazeBuildException("There is maze with negative size");
            }
            if (width % 2 != 1 || height % 2 != 1)
            {
                throw new MazeBuildException($"Width and height of MazeMap must be odd, yours is w:{width} and h:{height}");
            }
            if (width == 1 || height == 1)
            {
                throw new MazeBuildException($"Minimal width and height of MazeMap are 3 on 3, yours is w:{width} and h:{height}");
            }

            _currentSurface = new MazeMap(width, height);

            BuildGround();
            BuildWallMesh();

            MakeLabirint();

            BuildHero();

            return _currentSurface;
        }

        // labirint builder
        private void MakeLabirint()
        {
            var worker = new Worm(_currentSurface, _seed);
            worker.Start();
        }

        // hero
        private void BuildHero()
        {
            var hero = new Hero(1, 1, _currentSurface);
            hero.Hp = 10;
            hero.Money = 3;
            _currentSurface.Hero = hero;
        }

        // surface
        private void BuildWallMesh()
        {
            // y-axe walls
            for (int y = 0; y < _currentSurface.Height; y+=2)
            {
                for (int x = 0; x < _currentSurface.Width; x++)
                {
                    var wall = new Wall(x, y, _currentSurface);
                    _currentSurface.ReplaceCell(wall);
                }
            }
            // x-axe walls
            for (int y = 0; y < _currentSurface.Height; y ++)
            {
                for (int x = 0; x < _currentSurface.Width; x+=2)
                {
                    var wall = new Wall(x, y, _currentSurface);
                    _currentSurface.ReplaceCell(wall);
                }
            }
        }

        private void BuildGround()
        {
            for (int y = 0; y < _currentSurface.Height; y++)
            {
                for (int x = 0; x < _currentSurface.Width; x++)
                {
                    var ground = new Ground(x, y, _currentSurface);
                    _currentSurface.CellsSurface.Add(ground);
                }
            }
        }
    }
}
