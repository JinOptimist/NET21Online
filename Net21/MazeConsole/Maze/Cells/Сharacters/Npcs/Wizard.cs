using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsole.Maze.Cells.Сharacters.Npcs
{
    public class Wizard : BaseNpc
    {
        public Wizard(int x, int y, MazeMap mazeMap) : base(x, y, mazeMap)
        {
        }
        
        public override string Symbol => "?";

        public override BaseCell? CellToMove()
        {
            var nearCells = MazeMap.GetNearCell(this).Where(x => x is not Wall && x is not Coin);
            var hero = nearCells.OfType<Hero>().FirstOrDefault();
            if (hero != null) 
            {
                return hero;
            }
            return nearCells.First();
        }

        public override bool TryStep(BaseCharacter character)
        {
            character.Hp += 2;
            var ground = new Ground(X, Y, MazeMap);
            MazeMap.ReplaceCell(ground);
            return true;
        }
    }
}
