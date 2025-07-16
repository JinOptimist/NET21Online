using MazeConsole.Maze.Cells.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells
{
    /// <summary>
    /// Controls ocean sea mine generation and movement.
    /// Keeps constant mine count by respawning when needed,
    /// and periodically shuffles their locations.
    /// </summary>
    public class MineManager
    {
        private const int TotalMines = 2;
        private readonly MazeMap _maze;
        private readonly Random _random = new Random();

        private DateTime _lastSpawnCheck = DateTime.Now;
        private DateTime _lastMoveCheck = DateTime.Now;
        
        public MineManager(MazeMap maze)
        {
            _maze = maze;
            SpawnInitialMines();
        }

        public void Update()
        {
            // Mines spawn - every 2 seconds
            if ((DateTime.Now - _lastSpawnCheck).TotalSeconds >= 2)
            {
                _lastSpawnCheck = DateTime.Now;

                var currentMines = _maze.CellsSurface.OfType<Mine>().Count();
                if (currentMines < TotalMines)
                {
                    var seas = GetAvailableSea();
                    if (seas.Count > 0)
                    {
                        SpawnMine(seas);
                    }
                }
            }

            // Mine relocation — every 5 seconds
            if ((DateTime.Now - _lastMoveCheck).TotalSeconds >= 5)
            {
                _lastMoveCheck = DateTime.Now;

                foreach (var mine in _maze.CellsSurface.OfType<Mine>().ToList())
                {
                    mine.TryMove(_maze);
                }
            }
        }

        private void SpawnInitialMines()
        {
            var seas = GetAvailableSea();
            for (int i = 0; i < TotalMines && seas.Count > 0; i++)
            {
                SpawnMine(seas);
            }
        }

        private void SpawnMine(List<Sea> availableSea)
        {
            var sea = availableSea[_random.Next(availableSea.Count)];
            _maze.ReplaceCell(new Mine(sea.X, sea.Y, _maze));
            availableSea.Remove(sea);
        }



        private List<Sea> GetAvailableSea()
        {
            return _maze.CellsSurface
                .OfType<Sea>()
                .Where(s => !(s.X == _maze.Hero.X && s.Y == _maze.Hero.Y))
                .ToList();
        }
    }
}

