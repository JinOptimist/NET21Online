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

        // move worm
        public void MoveWarm()
        {
            X += 2;
            if (X >= _mazeMap.Width)
            {
                X = 1;
                Y += 2;
            }
            Console.WriteLine($"moved to {X}|{Y}");
        }

        // check way
        public bool CheckEast()
        {
            bool result = false;

            var cell = _mazeMap[X + 2, Y];
            if (cell != null)
            {
                result = true;
            }

            return result;
        }

        public bool CheckSouth()
        {
            bool result = false;

            var cell = _mazeMap[X, Y - 2];
            if (cell != null)
            {
                result = true;
            }

            return result;
        }

        // dig
        public void DigEast()
        {
            var ground = new Ground(X + 1, Y, _mazeMap);

            _mazeMap.ReplaceCell(ground);
        }

        public void DigSouth()
        {
            var ground = new Ground(X, Y - 1, _mazeMap);

            _mazeMap.ReplaceCell(ground);
        }

        // sub
        public void MakeDecision()
        {
            var s = CheckSouth();
            var e = CheckEast();


            if (s && e)
            {
                if (_random.Next(0, 2) == 0)
                {
                    DigSouth();
                }
                else
                {
                    DigEast();
                }
                Console.WriteLine("Rand dig");
            }
            else if (s)
            {
                DigSouth();
                Console.WriteLine("South dig");
            }
            else if (e)
            {
                DigEast();
                Console.WriteLine("East dig");
            }
            else
            {
                Console.WriteLine("Make decision exception");
            }
        }

        // start
        public void Start()
        {
            while (Y < _mazeMap.Height)
            {
                MakeDecision();
                MoveWarm();
                // Console.ReadKey();
            }
        }
    }
}
