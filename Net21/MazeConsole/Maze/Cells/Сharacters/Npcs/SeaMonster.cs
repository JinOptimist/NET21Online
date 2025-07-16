using MazeConsole.Maze.Cells.Surface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    /// <summary>
    /// Represents a sea monster that moves across ocean tiles (~),
    /// attacks the hero when nearby (deals -2 HP damage), and blocks the path.
    /// </summary>
    public class SeaMonster : BaseNpc
    {
        private Random _random = new Random();
        public SeaMonster(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
            Hp = 3;
        }

        public override string Symbol => "S";
        public override BaseCell? CellToMove()
        {
            var nearbyCells = MazeMap.
                GetNearCell(this);
            // Attack the hero if nearby
            var hero = nearbyCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                return hero;
            }

            var seaCells = nearbyCells
                .OfType<Sea>()
                .Where(cell => cell.X != cell.X || cell.Y != this.Y)
                .ToList();
            if (seaCells.Count == 0)
            {
                return null;
            }

            return seaCells[_random.Next(seaCells.Count)];
        }

        public override bool TryStep(BaseCharacter character)
        {
            if (character is Hero hero)
            {
                hero.Hp -= 2;
                return false;
            }

            return true;
        }
    }
}