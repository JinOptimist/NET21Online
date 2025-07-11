namespace MazeConsole.Maze.Cells
{
    /// <summary>
    /// Manages mines in the maze, ensuring consistent mine count and handling mine movement.
    /// </summary>
    public class MineManager
    {
        private const int TotalMines = 4;
        private readonly MazeMap _maze;
        private readonly Random _random = new Random();
        private DateTime _lastSpawnCheck = DateTime.Now;

        public MineManager(MazeMap maze)
        {
            _maze = maze;
            SpawnInitialMines();
        }

        private void SpawnInitialMines()
        {
            var grounds = GetAvailableGrounds();

            for (int i = 0; i < TotalMines && grounds.Count > 0; i++)
            {
                SpawnMine(grounds);
            }
        }

        private List<Ground> GetAvailableGrounds()
        {
            return _maze.CellsSurface
                .OfType<Ground>()
                .Where(g => g.X != 1 && g.Y != 1) // Не спавним на старте героя
                .ToList();
        }

        private void SpawnMine(List<Ground> availableGrounds)
        {
            var ground = availableGrounds[_random.Next(availableGrounds.Count)];
            _maze.ReplaceCell(new Mine(ground.X, ground.Y, _maze));
            availableGrounds.Remove(ground);
        }

        public void Update()
        {
            
            if ((DateTime.Now - _lastSpawnCheck).TotalSeconds >= 2)
            {
                _lastSpawnCheck = DateTime.Now;

                var currentMines = _maze.CellsSurface.OfType<Mine>().Count();
                if (currentMines < TotalMines)
                {
                    var grounds = GetAvailableGrounds();
                    if (grounds.Count > 0)
                    {
                        SpawnMine(grounds);
                    }
                }
            }

            // Обновляем позиции мин
            foreach (var mine in _maze.CellsSurface.OfType<Mine>().ToList())
            {
                mine.TryMove(_maze);
            }
        }
    }
}
