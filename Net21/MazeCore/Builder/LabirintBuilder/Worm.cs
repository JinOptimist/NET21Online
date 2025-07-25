using MazeCore.Maze;
using MazeCore.Maze.Cells.Surface;


namespace MazeCore.Builder.LabirintBuilder
{
    public class Worm
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private MazeMap _mazeMap;

        private Random _random;


        public Worm(MazeMap mazeMap, int seed)
        {
            X = 1;
            Y = 1;

            _mazeMap = mazeMap;

            _random = new Random(seed);
        }
        
        private void MoveWorm()
        {
            X += 2;
            if (X >= _mazeMap.Width)
            {
                X = 1;
                Y += 2;
            }
            Console.WriteLine($"moved to {X}|{Y}");
        }
        
        private bool Check(int x, int y)
        {
            bool result = false;

            var cell = _mazeMap[X + x, Y + y];
            if (cell != null)
            {
                result = true;
            }

            return result;
        }

        private void Dig(int x, int y)
        {
            var ground = new Ground(X + x, Y + y, _mazeMap);

            _mazeMap.ReplaceCell(ground);
        }
        
        private void MakeDecision()
        {
            var south = Check(0, -2);
            var east = Check(2, 0);


            if (south && east)
            {
                if (_random.Next(0, 2) == 0)
                {
                    Dig(0, -1);
                }
                else
                {
                    Dig(1, 0);
                }
                Console.WriteLine("Rand dig");
            }
            else if (south)
            {
                Dig(0, -1);
                Console.WriteLine("South dig");
            }
            else if (east)
            {
                Dig(1, 0);
                Console.WriteLine("East dig");
            }
            else
            {
                Console.WriteLine("No dig");
            }
        }
        
        public void Start()
        {
            while (Y < _mazeMap.Height)
            {
                MakeDecision();
                MoveWorm();
                // Console.ReadKey(); // Use ReadKey here to watch details of building
            }
        }
    }
}
