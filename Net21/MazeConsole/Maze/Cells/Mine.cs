using MazeConsole.Maze.Cells.Surface;
using MazeConsole.Maze.Cells.Сharacters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells
{
    /// <summary>
    /// A naval mine located in ocean waters (~).
    /// Detonates when touched by the hero (-3 HP) and is destroyed.
    /// Has random movement between adjacent sea cells.
    /// </summary>
    public class Mine : BaseCell
    {
        private readonly Random _random = new Random();

        public Mine(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }

        public override string Symbol => "x";

        public override bool TryStep(BaseCharacter character)
        {
            if (character is Hero hero)
            {
                hero.Hp -= 3;
                MazeMap.ReplaceCell(new Sea(X, Y, MazeMap)); 
                return true;
            }

            return false;
        }

        public void TryMove(MazeMap maze)
        {
            var seaNeighbors = maze
        .GetNearCell(this)
        .OfType<Sea>()
        .ToList();

            if (seaNeighbors.Count == 0)
                return;

            var target = seaNeighbors[_random.Next(seaNeighbors.Count)];

            maze.ReplaceCell(new Sea(X, Y, maze)); // очистка старой позиции
            X = target.X;
            Y = target.Y;
            maze.ReplaceCell(this); // установка новой мины

        }
    }
}
