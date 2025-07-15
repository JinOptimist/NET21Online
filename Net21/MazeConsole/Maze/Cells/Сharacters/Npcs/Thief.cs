using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Thief : BaseNpc
    {
        public int StolenMoney { get; set; }
        public Thief(int x, int y, MazeMap mazeMap, int hp = 6) : base(x, y, mazeMap)
        {
            Hp = hp;
        }

        public override string Symbol => "T";

        public override BaseCell? CellToMove()
        {
            var neraCells = MazeMap
                .GetNearCell(this);
            var hero = neraCells.OfType<Hero>().FirstOrDefault();
            if (hero != null)
            {
                return hero;
            }

            var grounds = MazeMap
                .GetNearCell(this)
                .OfType<Ground>();
            if (!grounds.Any())
            {
                return null;
            }

            return grounds.First();
        }

        public override bool TryStep(BaseCharacter character)
        {
            if (character is Hero hero)
            {
                StolenMoney = hero.Money;
                hero.Money = 0;
                return true;
            }
            return false;
        }
    }
}
